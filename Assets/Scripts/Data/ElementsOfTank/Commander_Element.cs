using TanksEngine.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "New Commander", menuName = "PlayerTankData/Commander")]
public class Commander_Element : ScriptableObject
{
    public PlayerTankData.Commander commander;
}
