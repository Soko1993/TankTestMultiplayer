using TanksEngine.Character;
using System;
using UnityEngine;

public class Global_EventManager : MonoBehaviour
{
    public static Action<AttributeTypes> eventOnAttributeUpdate;
    public static Action<GameObject> eventOnEnemyDie;
    public static Action<GameObject> eventOnPlayerLoaded;
    public static Action<TanksEngine.UI.Game_UI> eventOnGameUiLoaded;
    public static Action eventOnPlayerDie;
    public static Action eventOnCoinsUpdate;
    public static Action eventOnGameWin;
    public static Action eventOnGameLose;
    public static Action eventSaveGame;
    public static Action eventOnLoadGame;
    public static Action eventOnSetupMainData;
    public static Action<GameObject> eventOnGetGameCore;

    public static void CallOnAttributeUpdate(AttributeTypes type)
    {
        eventOnAttributeUpdate?.Invoke(type);
    }
    public static void CallOnEnemyDie(GameObject enemy)
    {
        eventOnEnemyDie?.Invoke(enemy);
    }
    public static void CallOnPlayerDie()
    {
        eventOnPlayerDie?.Invoke();
    }
    public static void CallOnGameLose()
    {
        eventOnGameLose?.Invoke();
    }
    public static void CallOnGameWin()
    {
        eventOnGameWin?.Invoke();
    }
    public static void CallOnCoinsUpdate()
    {
        eventOnCoinsUpdate?.Invoke();
    }
    public static void CallOnPlayerLoaded(GameObject player)
    {
        eventOnPlayerLoaded?.Invoke(player);
    }
    public static void CallOnGameUILoaded(TanksEngine.UI.Game_UI gameUI)
    {
        eventOnGameUiLoaded?.Invoke(gameUI);
    }
    public static void CallOnSaveGame()
    {
        eventSaveGame?.Invoke();
    }
    public static void CallOnLoadGame()
    {
        eventOnLoadGame?.Invoke();
    }
    public static void CallOnSetupMainData()
    {
        eventOnSetupMainData?.Invoke();
    }
    public static void CallOnGetGameCore(GameObject obj)
    {
        eventOnGetGameCore?.Invoke(obj);
    }
}



