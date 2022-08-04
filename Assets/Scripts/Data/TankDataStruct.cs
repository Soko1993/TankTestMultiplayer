using static TanksEngine.Data.PlayerTankData;
[System.Serializable]
public struct TankDataStruct
{
    public Tank_Detail detailMain;
    public Tank_Detail detailEngine;
    public Tank_Detail detailTurret;
    public Tank_Detail detailSuspension;
    public Commander commander;
}
