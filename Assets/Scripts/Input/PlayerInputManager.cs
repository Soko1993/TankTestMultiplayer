using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput Input { get; private set; }


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
        //Input.Player.Shoot.performed += context => GetComponent<PlayerCombat>().Shoot();
    }
}
