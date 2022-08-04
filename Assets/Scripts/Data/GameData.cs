using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int AngarSlotsCount;
    public static int CurSlot;
    public static List<PlayerTankData> AngarSlots = new List<PlayerTankData>(5);
    public static void Init()
    {
        PlayerTankData a = new();
        a.commanderList = new();
        a.detailEngineList = new();
        a.detailMainList = new();
        a.detailSuspensionList = new();
        a.detailTurretList = new();
        a.tankDataStruct = new();
        AngarSlots.Add(a);
    }
}
