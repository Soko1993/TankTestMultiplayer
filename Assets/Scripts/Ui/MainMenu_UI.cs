using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TanksEngine.Data.PlayerTankData;
/// <summary>
/// Обрабатывает взаимодействие игрока
/// с главным меню.
/// </summary>
namespace TanksEngine.UI
{
    public class MainMenu_UI : MonoBehaviour
    {
        [Header("Main")]
        public GameObject[] MenuPanels;
        public GameObject LoadingScreen;
        public GameObject CoinsText;
        public GameObject DetailsPanel;
        public GameObject detailButton;
        public GameObject startButton;
        public GameObject playerModel;
        public GameObject myCommanderNameText;
        public GameObject myCommanderImage;
        public GameObject CurrentDetailsPanel;
        public GameObject PlayerDetailsPanel;
        public Text AngarSlotText;
       
        public List<DetailCurrent_Struct_UI> current_details = new List<DetailCurrent_Struct_UI>();
        public Sprite ClearImage;
        public GameObject panel_button_obj;
        [Header("Buttons to manage tank info")]
        public GameObject ShowCurrentDetailsButton;
        public GameObject CommanderButton;
        private void OnEnable()
        {
            //MainMenu.InitMenu(this);
            Global_EventManager.eventOnLoadGame += onLoadGame;
            if (MainMenu.SkipMenu)
            {
                Invoke(nameof(onLoadGame),0.5f);
            }
        }

        private void OnDestroy()
        {
            Global_EventManager.eventOnLoadGame -= onLoadGame;
        }

        private void Start()
        {
            LoadingScreen.SetActive(true);
        }

        public void ClickStartButton()
        {
            SceneManager.LoadScene(3);
        }

        public void ClickSaveGame()
        {
            Global_EventManager.CallOnSaveGame();
        }

        public void DeleteAllSavedData()
        {

        }
        public void ClickGameExit()
        {
            Application.Quit();
        }
        private void OnApplicationQuit()
        {

        }
        private void onLoadGame()
        {
            LoadingScreen.SetActive(false);
            MainMenu.InitMenu(this);
        }
        public void ClickPlayerDetails()
        {
            MainMenu.ShowPlayerDetailsPanel();
        }
        public void ClickCurrentDetail(int typeDetail)
        {
            MainMenu.UpdatePlayerDetails(typeDetail);
        }
        public void ClickSwitchPanel(int id) //переключение между вкладками меню
        {
            MainMenu.SelectPanel(id);
        }
        public void ClickCommander()
        {
            MainMenu.ShowCommanders();

        }
        public void ClickChangeAngarSlot(bool Next)
        {
            if (Next)
            {
                MainMenu.NextAngarSlot();
            }
            else
            {
                MainMenu.PrevAngarSlot();
            }

        }
    }

}
