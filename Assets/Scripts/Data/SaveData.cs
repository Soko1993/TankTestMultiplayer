using TanksEngine.Data;
using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    public List<PlayerTankData.Tank_Detail> Details_Main;
    public List<PlayerTankData.Commander> Commanders;
    public List<PlayerTankData> AngarSlots;
}
