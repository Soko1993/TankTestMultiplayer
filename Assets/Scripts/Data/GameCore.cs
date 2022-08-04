using TanksEngine.Data;
using TanksEngine.SaveLoad;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance { get { return _instance; } }
    public Main_Data MainData;
    public bool SkipMainMenu;

    private string saveFilePath;
    private string saveFileName = "/SaveFile.json";
    private static GameCore _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        Global_EventManager.eventOnPlayerLoaded += OnPlayerLoaded;
        Global_EventManager.eventSaveGame += OnGameSave;
    }

    private void OnDestroy()
    {
        Global_EventManager.eventOnPlayerLoaded -= OnPlayerLoaded;
        Global_EventManager.eventSaveGame -= OnGameSave;
    }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        //saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        saveFilePath = Application.dataPath + saveFileName;
#endif
        DontDestroyOnLoad(gameObject);
        Invoke(nameof(TryLoadGameData), 2);
    }
    private void OnPlayerLoaded(GameObject player)
    {
        player.GetComponent<TankDetailsManager>().tankDataStruct.detailMain = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.detailMain;
        player.GetComponent<TankDetailsManager>().tankDataStruct.commander = GameData.AngarSlots[GameData.CurSlot].tankDataStruct.commander;
    }
    public void TryLoadGameData()
    {
        SaveLoadSystem.Load_GameData(saveFilePath, MainData);
        SaveLoadSystem.Save_GameData(saveFilePath, true);
    }
    private void OnGameSave()
    {
        SaveLoadSystem.Save_GameData(saveFilePath);
    }
    public void DeleteAllSavedData()
    {

    }
    
}
