using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float MaxCameraPos;
    public float MinCameraPos;

    public float CamPosY;
    public float CamPosX;

    public float DistanceToPlayer;

    public bool IgnoreZone;

    public Vector3 CamRotation;
    private void Start()
    {
        if (Player != null) { transform.LookAt(Player); }
        transform.eulerAngles = CamRotation;
    }
    void Update()
    {
        if (Player != null)
        {
            if(Player.transform.position.z > MinCameraPos && Player.transform.position.z < MaxCameraPos || IgnoreZone)
            {
                transform.position = new Vector3(Player.position.x, CamPosY, Player.position.z- DistanceToPlayer);
                //transform.LookAt(Player);
            }
        }
        
        
    }
}
