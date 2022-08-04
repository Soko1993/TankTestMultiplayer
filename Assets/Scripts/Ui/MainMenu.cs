using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TanksEngine.Data.PlayerTankData;

/// <summary>
/// Реализует функционал, связанный 
/// с главным меню.
/// </summary>
namespace TanksEngine.UI
{
    public static class MainMenu
    {
        private static List<Tank_Detail> Details_Main = new();
        private static List<Tank_Detail> Details_Turret = new();
        private static List<Tank_Detail> Details_Engine = new();
        private static List<Tank_Detail> Details_Suspension = new();
        private static List<Commander> Commanders = new();

        public static MainMenu_UI mainMenu_UI;
        public static bool SkipMenu = false;

        /// <summary>
        /// Инициализация, вызов отображения
        /// главного меню.
        /// </summary>
        /// <param name="menu"></param>
        public static void InitMenu(MainMenu_UI menu)
        {
            mainMenu_UI = menu;
            GetGameData();

            if (SkipMenu)
            {
                mainMenu_UI.LoadingScreen.SetActive(false);
                SelectPanel(1);
            }
            else
            {
                mainMenu_UI.LoadingScreen.SetActive(false);
                SelectPanel(0);
            }

            UpdateAngarPanel();
            CheckTankParts();
            
        }
        public static void NextAngarSlot()
        {
            if (GameData.CurSlot+1 < GameData.AngarSlotsCount)
            {
                GameData.CurSlot++;
            }
            else
            {
                while (GameData.CurSlot >= GameData.AngarSlotsCount)
                {
                    GameData.CurSlot--;
                }
            }
            UpdateAngarPanel();
        }
        public static void PrevAngarSlot()
        {
            if (GameData.CurSlot > 0)
            {
                GameData.CurSlot--;
            }
            else
            {
                while (GameData.CurSlot < 0)
                {
                    GameData.CurSlot++;
                }
            }
            UpdateAngarPanel();
        }
        private static void GetGameData()
        {

            Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
            Details_Turret = GameData.AngarSlots[GameData.CurSlot].detailTurretList;
            Details_Engine = GameData.AngarSlots[GameData.CurSlot].detailEngineList;
            Details_Suspension = GameData.AngarSlots[GameData.CurSlot].detailSuspensionList;
        }
        /// <summary>
        /// Обновляет панель ангара.
        /// Закрывает все панельки
        /// </summary>
        public static void UpdateAngarPanel()
        {
            UpdateCurrentDetailsPanel();
            int idx = GameData.CurSlot + 1;
            mainMenu_UI.AngarSlotText.text = "Ангар: "+idx.ToString();

            if (GameData.AngarSlots[GameData.CurSlot].slotType == AngarSlotType.Empty)
            {
                //Debug.Log("Empty slot!");
                MenuTankDisplayer.ClearTankViusal(mainMenu_UI.playerModel);
                mainMenu_UI.ShowCurrentDetailsButton.SetActive(false);
                mainMenu_UI.CommanderButton.SetActive(false);
            }
            else
            {
                MenuTankDisplayer.UpdateTankVisual(GameData.CurSlot, mainMenu_UI.playerModel);
                mainMenu_UI.ShowCurrentDetailsButton.SetActive(true);
                mainMenu_UI.CommanderButton.SetActive(true);
            }
            CheckTankParts();
        }
        /// <summary>
        /// Обновляет панель с теущими деталями.
        /// </summary>
        /// <param name="typeDetail"></param>
        private static void UpdateCurrentDetailsPanel(int typeDetail = 0)
        {
            ClearPlayerDetailsPanel();

            Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
            Commanders = GameData.AngarSlots[GameData.CurSlot].commanderList;

            int detailsCount = 4;
            Tank_Detail detail = new();
            string detailType = "";

            //Обновляет инфу на кнопках текущих деталей
            for (int i = 0; i < detailsCount; i++)
            {
                switch (i)
                {
                    case 0:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain;
                            detailType = "Основа";
                        }
                        break;
                    case 1:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine;
                            detailType = "Движок";
                        }
                        break;
                    case 2:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension;
                            detailType = "Подвеска";
                        }
                        break;
                    case 3:
                        if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret.Name != "")
                        {
                            detail = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret;
                            detailType = "Пушка";
                        }
                        break;
                    default:
                        break;
                }
                if (detail.Name != null)
                {
                    mainMenu_UI.current_details[i].text.text = $"{detailType}: {detail.Name}";
                    mainMenu_UI.current_details[i].image.sprite = detail.Icon;
                    MenuTankDisplayer.UpdateTankVisual(GameData.CurSlot, mainMenu_UI.playerModel);
                }
                else
                {
                    mainMenu_UI.current_details[i].text.text = $"Деталь {detailType.ToLower()}";
                }
            }

            //Обновление информации о выбранном командире
            if (GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Name != null)
            {
                mainMenu_UI.myCommanderNameText.GetComponent<Text>().text = $"{GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Name}";
                mainMenu_UI.myCommanderImage.GetComponent<Image>().sprite = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Icon;
            }
            else
            {
                mainMenu_UI.myCommanderNameText.GetComponent<Text>().text = "Выберите командира.";
                mainMenu_UI.myCommanderImage.GetComponent<Image>().sprite = mainMenu_UI.ClearImage;
            }
            //Обновление панели со списком деталей для выбора
            UpdatePlayerDetails(typeDetail);
        }
        /// <summary>
        /// Очищает панельку с 
        /// деталями для выбора.
        /// </summary>
        public static void ClearPlayerDetailsPanel()
        {
            foreach (Transform child in mainMenu_UI.DetailsPanel.transform) //Очистка панели со списком деталей
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        /// <summary>
        /// Показывает панель с деталями для выбора.
        /// </summary>
        public static void ShowPlayerDetailsPanel()
        {
            if (mainMenu_UI.CurrentDetailsPanel.activeSelf)
            {
                mainMenu_UI.CurrentDetailsPanel.SetActive(false);
                mainMenu_UI.PlayerDetailsPanel.SetActive(false);
            }
            else
            {
                mainMenu_UI.CurrentDetailsPanel.SetActive(true);
                mainMenu_UI.PlayerDetailsPanel.SetActive(true);
            }
        }
        /// <summary>
        /// Меняет текущую деталь на выбранную.
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="detailType"></param>
        public static void SelectDetail(Tank_Detail detail, int detailType)
        {
            PlayerTankData newData = GameData.AngarSlots[GameData.CurSlot];
            switch (detailType)
            {
                case 0:
                    newData.tankDataStruct.detailMain = detail;
                    break;
                case 1:
                    newData.tankDataStruct.detailEngine = detail;
                    break;
                case 2:
                    newData.tankDataStruct.detailSuspension = detail;
                    break;
                case 3:
                    newData.tankDataStruct.detailTurret = detail;
                    break;
                default:
                    break;
            }
            GameData.AngarSlots[GameData.CurSlot] = newData;
            CheckTankParts();
            UpdateCurrentDetailsPanel(detailType);
        }
        /// <summary>
        /// Создает новую кнопку для выбора детали.
        /// </summary>
        /// <param name="item">Структура с инфой о детали</param>
        /// <param name="name">Название этой детали</param>
        /// <param name="type">Тип детали.</param>
        private static void SpawnButtonDetail(Tank_Detail item, string name, int type)
        {
            mainMenu_UI.panel_button_obj = GameObject.Instantiate(mainMenu_UI.detailButton);
            mainMenu_UI.panel_button_obj.transform.SetParent(mainMenu_UI.DetailsPanel.transform);
            mainMenu_UI.panel_button_obj.transform.localScale = new Vector3(1, 1, 1);

            mainMenu_UI.panel_button_obj.transform.GetChild(0).GetComponent<Text>().text = $"{name}: {item.Name}";
            mainMenu_UI.panel_button_obj.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
            mainMenu_UI.panel_button_obj.GetComponent<Button>().onClick.AddListener(() => SelectDetail(item, type));
        }
        private static void ShowDetailsMain()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in Details_Main)
            {
                SpawnButtonDetail(item, "Основа", 0);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsEngine()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in Details_Engine)
            {
                SpawnButtonDetail(item, "Двигатель", 1);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailEngine.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsSuspension()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in Details_Suspension)
            {
                SpawnButtonDetail(item, "Подвеска", 2);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailSuspension.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        private static void ShowDetailsTurret()
        {
            ClearPlayerDetailsPanel();
            foreach (Tank_Detail item in Details_Turret)
            {
                SpawnButtonDetail(item, "Пушка", 3);

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailTurret.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        /// <summary>
        /// Реалзиует переключение между
        /// панелями главного меню.
        /// </summary>
        /// <param name="id"></param>
        public static void SelectPanel(int id)
        {
            foreach (GameObject item in mainMenu_UI.MenuPanels)
            {
                item.SetActive(false);
            }

            switch (id)
            {
                case 0://Главное меню
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    break;
                case 1://Ангар
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    UpdateAngarPanel();
                    break;
                case 2://Настройки
                    mainMenu_UI.MenuPanels[id].SetActive(true);
                    break;
            }
        }
        /// <summary>
        /// Активирует кнопку "В бой" если
        /// все компоненты танка установлены.
        /// </summary>
        private static void CheckTankParts()
        {
            var data = GameData.AngarSlots[GameData.CurSlot].tankDataStruct;
            if (data.detailSuspension.Name != null && data.detailMain.Name != null && data.detailEngine.Name != null && data.detailTurret.Name != null && data.commander.Name != null)
            {
                mainMenu_UI.startButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                mainMenu_UI.startButton.GetComponent<Button>().interactable = false;
            }
        }
        public static void UpdatePlayerDetails(int typeDetail = 0)
        {
            switch (typeDetail)
            {
                case 0:
                    ShowDetailsMain();
                    break;
                case 1:
                    ShowDetailsEngine();
                    break;
                case 2:
                    ShowDetailsSuspension();
                    break;
                case 3:
                    ShowDetailsTurret();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Выводит список доступных командиров
        /// </summary>
        public static void ShowCommanders()
        {
            ClearPlayerDetailsPanel();
            mainMenu_UI.PlayerDetailsPanel.SetActive(true);

            //Обновление панели со списком деталей
            foreach (Commander item in Commanders) //спавн кнопок для выбора танков
            {
                //Debug.Log(item);
                mainMenu_UI.panel_button_obj = GameObject.Instantiate(mainMenu_UI.detailButton);
                mainMenu_UI.panel_button_obj.transform.SetParent(mainMenu_UI.DetailsPanel.transform);
                mainMenu_UI.panel_button_obj.transform.localScale = new Vector3(1, 1, 1);

                mainMenu_UI.panel_button_obj.transform.GetChild(0).GetComponent<Text>().text = $"{item.Name}";
                mainMenu_UI.panel_button_obj.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
                mainMenu_UI.panel_button_obj.GetComponent<Button>().onClick.AddListener(() => SelectCommander(item));

                if (item.Name == GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander.Name)
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.green;
                }
                else
                {
                    mainMenu_UI.panel_button_obj.GetComponent<Outline>().effectColor = Color.black;
                }
            }
        }
        /// <summary>
        /// Заменяет текущего командира
        /// на выбранного.
        /// </summary>
        /// <param name="commander"></param>
        private static void SelectCommander(Commander commander)
        {
            PlayerTankData newData = GameData.AngarSlots[GameData.CurSlot];
            newData.tankDataStruct.commander = commander;
            GameData.AngarSlots[GameData.CurSlot] = newData;
            CheckTankParts();
            UpdateCurrentDetailsPanel();
            mainMenu_UI.PlayerDetailsPanel.SetActive(false);
        }
    }

}
