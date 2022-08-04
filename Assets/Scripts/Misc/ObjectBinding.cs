using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBinding : MonoBehaviour
{
    public Transform BindToGO;
    public Vector3 Offset;

    public bool FindPlayerOnStart;

    public float FindObjFrequency = 1;

    private float _timer;
    private void Update()
    {
        if (FindPlayerOnStart)
        {
            if (BindToGO == null)
            {
                _timer += Time.deltaTime;
                if (_timer > FindObjFrequency)
                {
                    _timer -= FindObjFrequency;

                    Debug.Log("try find");
                    if (FindPlayerOnStart)
                    {
                        if (BindToGO == null)
                        {
                            if (FindObjWithTag("Player") != null)
                            {
                                BindToGO = FindObjWithTag("Player");
                            }
                        }
                    }
                }
            }
        }


        if (BindToGO != null)
        {
            transform.position = new Vector3(BindToGO.position.x + Offset.x, BindToGO.position.y + Offset.y, BindToGO.position.z + Offset.z);
        }
    }
    private Transform FindObjWithTag(string tag)
    {
        var go = GameObject.FindGameObjectWithTag(tag);
        if (go != null)
        {
            return go.transform;
        }
        else
        {
            return null;
        }
    }
}
