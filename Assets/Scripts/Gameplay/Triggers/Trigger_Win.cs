using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Win : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            Global_EventManager.CallOnGameWin();
        }
    }
}
