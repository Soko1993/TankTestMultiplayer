using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_GameArena : MonoBehaviour
{
    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Global_EventManager.CallOnGameLose();
        }
    }
}
