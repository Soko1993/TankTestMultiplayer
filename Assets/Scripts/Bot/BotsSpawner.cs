using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class BotsSpawner 
{
    public static void SpawnBots(GameObject[] Enemies, GameObject[] SpawnPoints)
    {
        int i = 0;
        foreach(var item in Enemies)
        {
            if (i < SpawnPoints.Length)
            {
                GameObject obj = GameObject.Instantiate(item, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                obj.GetComponent<NavMeshAgent>().avoidancePriority = i;
                i++;
            }
            else
            {
                i = 0;
                GameObject.Instantiate(item, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
            }
        }
    }
}
