using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LevelsStorage new", menuName = "GameData/LevelsStorage")]
public class LevelsStorageSO : ScriptableObject
{
    public LevelDataSO[] LevelStorage;
}
