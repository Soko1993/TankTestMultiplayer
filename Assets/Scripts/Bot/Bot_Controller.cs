using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot_Controller : MonoBehaviour
{
    public bool CanMove;
    private float _timer = 0f;
    
    private Vector3 startpos;
    private NavMeshHit navMeshHit;
    private NavMeshPath navPath;
    private bool inPoint;
    private Vector3 point;

    public NavMeshAgent nav;
    public float PatrolRadius;
    public float _wait = 1.5f;

    void Start()
    {
        if (CanMove)
        {
            navPath = new NavMeshPath();
            startpos = transform.position;
            
        }
        _timer = _wait + 1;
    }

    void Update()
    {
        if (CanMove)
        {
            _timer += Time.deltaTime;

            if (_timer > _wait)
            {
                _timer -= _wait;
                try
                {
                    NavMesh.SamplePosition(Random.insideUnitSphere * 15f + startpos, out navMeshHit, 15f, NavMesh.AllAreas);
                    point = navMeshHit.position;

                    nav.CalculatePath(point, navPath);
                    //Debug.Log(point);
                    nav.SetDestination(point);
                }
                catch
                {
                    Debug.LogError("Error when npc try move");
                }


            }
        }
        
    }
}