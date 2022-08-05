using TanksEngine.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

/// <summary>
/// Реализует управление танком
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterEntity))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private CharacterEntity pAttr;
    public bool isMoving;
    public float moveSpeed;

    PhotonView view;
    //public string Name;

    private FloatingJoystick _joystick;

    void Start()
    {
        view = GetComponent<PhotonView>();
        //Name = "Player " + Random.Range(1000, 9999);
        Debug.Log ("PlayerController");
    }

    private void InitJoystick(TanksEngine.UI.Game_UI gameUI)
    {
        _joystick = gameUI.Joystick;
    }
    private void OnEnable()
    {
        Global_EventManager.eventOnAttributeUpdate += HandlerOnAttributeUpdate;
        Global_EventManager.eventOnGameUiLoaded += InitJoystick;
    }

    private void OnDisable()
    {
        Global_EventManager.eventOnAttributeUpdate -= HandlerOnAttributeUpdate;
        Global_EventManager.eventOnGameUiLoaded -= InitJoystick;
    }

    private void HandlerOnAttributeUpdate(AttributeTypes type)
    {
        switch (type)
        {
            case 0:
                //moveSpeed = pAttr.Speed;
                break;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        pAttr = GetComponent<CharacterEntity>();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if (view.IsMine) 
        {
            return;
        }

        //_rb.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rb.velocity.y, _joystick.Vertical * moveSpeed);
        if (_joystick != null)
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                isMoving = true;
                Vector3 move = new Vector3(_joystick.Horizontal, _rb.velocity.y, _joystick.Vertical);
                _rb.AddForce(move * moveSpeed, ForceMode.Force);

                if (_rb.velocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(_rb.velocity);
                }
            }
            else
            {
                isMoving = false;
            }

        }
    }

}
