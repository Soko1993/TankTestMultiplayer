using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameResources Data", menuName = "PlayerTankData/Game_Resources")]
public class GameResourcesData : ScriptableObject
{
    public GameObject Player;
    public GameObject[] Enemies;
    public GameObject GameUI;
    public GameObject MainCamera;
}
