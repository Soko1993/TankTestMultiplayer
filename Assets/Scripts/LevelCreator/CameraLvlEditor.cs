using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLvlEditor : MonoBehaviour
{
    public float Speed;
   
    public PlayerInput Input;

    private Vector2 direction;
    private float scaledSpeed;

    private void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }
    private void Awake()
    {
        Input = new PlayerInput();
    }
    private void FixedUpdate()
    {
        direction = Input.Player.Move.ReadValue<Vector2>();
        if (direction.x != 0 || direction.y != 0)
        {
            Movement();
        }
    }
    private void Movement()
    {
        scaledSpeed = Speed * Time.deltaTime;
        transform.position += new Vector3(direction.x, 0, direction.y) * scaledSpeed;
    }
}
