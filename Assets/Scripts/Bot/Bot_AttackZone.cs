using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_AttackZone : MonoBehaviour
{
    public Bot_Combat BotCombat;
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            BotCombat.cur_target = other.gameObject;
        }
    }
}
