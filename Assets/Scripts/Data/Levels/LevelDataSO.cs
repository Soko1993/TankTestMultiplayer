using UnityEngine;
[CreateAssetMenu(fileName = "New LevelData", menuName = "GameData/LevelData")]
public class LevelDataSO :ScriptableObject
{
    public TanksEngine.LevelCreator.LevelData levelData;
}
