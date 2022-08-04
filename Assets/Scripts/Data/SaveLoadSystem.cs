using System.Collections;
using System.Collections.Generic;
using System.IO;
using TanksEngine.Data;
using UnityEngine;

namespace TanksEngine.SaveLoad
{
    /// <summary>
    /// Реализует сохранение и загрузку игровых данных.
    /// </summary>
    public static class SaveLoadSystem
    {
        public static void Load_GameData(string saveFilePath, Main_Data MainData)
        {
            if (File.Exists(saveFilePath))  //Если существует сохранение, то загрузить его
            {
                Debug.Log("Load game");
                try
                {
                    string json = File.ReadAllText(saveFilePath);
                    SaveData data = JsonUtility.FromJson<SaveData>(json);

                    GameData.AngarSlots = data.AngarSlots;
                }
                catch
                {
                    Debug.LogError("Can't load save_data file");
                }
            }
            else
            {
                Debug.Log("Save file not finded");
                int idx = 0;
                while (idx < MainData.AngarSlots)
                {
                    GameData.Init();
                    GameData.AngarSlotsCount = MainData.AngarSlots;

                    //Заполняем данные(списки деталей для выбора) для игрока из бд (json файл)
                    foreach (Detail_Main_Element item in MainData.Details_Main)
                    {
                        GameData.AngarSlots[idx].detailMainList.Add(item.detail_main);
                    }
                    foreach (Detail_Engine_Element item in MainData.Details_Engine)
                    {
                        GameData.AngarSlots[idx].detailEngineList.Add(item.detail_engine);
                    }
                    foreach (Detail_Suspension_Element item in MainData.Details_Suspension)
                    {
                        GameData.AngarSlots[idx].detailSuspensionList.Add(item.detail_suspension);
                    }
                    foreach (Detail_Turret_Element item in MainData.Details_Turret)
                    {
                        GameData.AngarSlots[idx].detailTurretList.Add(item.detail_turret);
                    }
                    foreach (Commander_Element item in MainData.Commanders)
                    {
                        GameData.AngarSlots[idx].commanderList.Add(item.commander);
                    }
                    idx++;
                    
                }
                int tempindx = 0;
                PlayerTankData newData2 = GameData.AngarSlots[tempindx];
                newData2.slotType = AngarSlotType.LightTank;
                newData2.tankDataStruct.detailMain = GameData.AngarSlots[tempindx].detailMainList[0];
                newData2.tankDataStruct.detailEngine = GameData.AngarSlots[tempindx].detailEngineList[0];
                newData2.tankDataStruct.detailSuspension = GameData.AngarSlots[tempindx].detailSuspensionList[0];
                newData2.tankDataStruct.detailTurret = GameData.AngarSlots[tempindx].detailTurretList[0];
                GameData.AngarSlots[tempindx] = newData2;
            }
            Global_EventManager.CallOnLoadGame();
        }
        public static void Save_GameData(string saveFilePath, bool test = false)
        {
            Debug.Log("Save game");
            SaveData savdata = new SaveData();
            savdata.Details_Main = GameData.AngarSlots[GameData.CurSlot].detailMainList;
            savdata.Commanders = GameData.AngarSlots[GameData.CurSlot].commanderList;
            savdata.AngarSlots = GameData.AngarSlots;
            try
            {
                string json = JsonUtility.ToJson(savdata, true);
                if (test)
                {
                    File.WriteAllText(Application.dataPath + @"\TestSave.txt", json);
                }

                else
                {
                    File.WriteAllText(saveFilePath, json);
                }

            }
            catch
            {
                Debug.LogError("Can't save save_data file");
            }
        }
    }

}
