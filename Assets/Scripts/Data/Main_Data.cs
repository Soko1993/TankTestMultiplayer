using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Main Data", menuName = "PlayerTankData/Main_Data")]
public class Main_Data : ScriptableObject
{
    public List<Detail_Main_Element> Details_Main;
    public List<Detail_Engine_Element> Details_Engine;
    public List<Detail_Suspension_Element> Details_Suspension;
    public List<Detail_Turret_Element> Details_Turret;

    public List<Commander_Element> Commanders;

    public int AngarSlots;
}
