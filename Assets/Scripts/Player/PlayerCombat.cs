using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterEntity))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCombat : MonoBehaviour
{
    #region[Variables]
    [Header("Turret objects")]
    [Tooltip("")]
    public GameObject Weapon;
    public GameObject WeaponIdle;
    public GameObject TurretPoint;
    public GameObject bullet_model;
    [Header("Components")]
    public TankTurret tankTurret;
    public TankDetailsManager PlayerInfo;
    public Rigidbody rb;
    public PlayerController _Controller;
    public CharacterEntity _pAttr;

    [HideInInspector]
    public List<GameObject> EnemyToAttack = new(5);
    [HideInInspector]
    public GameObject cur_target;
    [HideInInspector]
    public GameObject bullet;

    private float _timer = 0f;
    private float _shotFrequency  = 0.3f;//Задержка выстрелов

    #endregion

    private void Awake()
    {
        _pAttr = GetComponent<CharacterEntity>();
        
    }
    private void Start()
    {
        tankTurret.player = this; 
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _shotFrequency)
        {
            _timer -= _shotFrequency;

            if (EnemyToAttack.Count != 0) //&& !_Controller.isMoving
            {
                foreach (var item in EnemyToAttack.ToList())
                {
                    if (item != null)
                    {
                        cur_target = item;
                        break;
                    }
                    else
                    {
                        EnemyToAttack.Remove(item);
                        
                    }
                    cur_target = null;
                }
                //CreateBullet();
                
            }
            else
            {
                cur_target = null;
            }
        }
        tankTurret.Aiming();
    }
  

}
