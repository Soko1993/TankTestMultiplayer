using System.Collections;
using System.Collections.Generic;
using TanksEngine.Character;
using UnityEngine;
/// <summary>
/// Реализует систему скрытности
/// </summary>
public class PlayerStealthController : MonoBehaviour, ICanStealth
{
    public bool IsStealthy { get; set; }
    public void DisableStealt()
    {
        IsStealthy = false;
        Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Stealth);
    }
    public void EnableStealt()
    {
        IsStealthy = true;
        Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Stealth);
    }

}
