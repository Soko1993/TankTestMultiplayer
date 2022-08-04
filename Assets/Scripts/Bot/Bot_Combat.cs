using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(CharacterEntity))]
public class Bot_Combat : MonoBehaviour
{
    
    public float range = 30;
    public float TurretSpeed = 60f;//брать будем из so
    public float ShotFrequency = 0.2f;//брать будем из so
    public bool AimingDone;

    public Transform WeaponIdle;
    public Transform TurretPoint;
    public Transform BulletPoint;
    public GameObject cur_target;
    public GameObject bullet_model;
    public float Damage;
    public float MaxDistance;
    public Collider DetectCollider;

    private RaycastHit hit;
    private Vector3 direction;
    private Vector3 idledirection;
    private float _timer;

    private float singleStep;

    private Vector3 targetDirection;
    private Vector3 newDirection;
    private GameObject bullet;
    private CharacterEntity characterEntity;

    public void Awake()
    {
        cur_target = null;
        characterEntity = GetComponent<CharacterEntity>();
    }

    public void Update()
    {
        if (characterEntity.IsAlive)
        {
            Aiming();
        }
       
    }
    public UnityEvent OnShoot;
    /// <summary>
    /// Осуществляет прицеливание
    /// </summary>
    public void Aiming()
    {
        idledirection = WeaponIdle.position;
        direction = TurretPoint.forward;
        //Если луч задел цель -> делаем выстрел
        
        if (Physics.Raycast(TurretPoint.position, TurretPoint.transform.forward * range, out hit, range, 9))
        {
            Debug.DrawRay(TurretPoint.position, TurretPoint.transform.forward * range,Color.blue);
            //Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                Shoot();
                AimingDone = true;
            }
        }
        else
        {
            Debug.DrawRay(TurretPoint.position, TurretPoint.transform.forward * range);
            AimingDone = false;
        }
        //Если есть цель -> поворачиваем башню в её сторону
        if (cur_target != null)
        {
            if (Vector3.Distance(transform.position, cur_target.transform.position) > MaxDistance)
            {
                cur_target = null;
            }
            else
            {
                if (!cur_target.GetComponent<ICanStealth>().IsStealthy)
                {
                    singleStep = TurretSpeed * Time.deltaTime;
                    targetDirection = cur_target.transform.position - TurretPoint.transform.position;
                    newDirection = Vector3.RotateTowards(TurretPoint.transform.forward, targetDirection, singleStep, 0.0f);
                    TurretPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);
                }

            }

        }
        else
        {
            
            singleStep = TurretSpeed * Time.deltaTime;
            targetDirection = WeaponIdle.transform.position - TurretPoint.transform.position;
            newDirection = Vector3.RotateTowards(TurretPoint.transform.forward, targetDirection, singleStep, 0.0f);
            TurretPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);

        }
    }
    /// <summary>
    /// Реализует выстрел
    /// </summary>
    private void Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > ShotFrequency)
        {
            _timer -= ShotFrequency;
            if (cur_target != null)
            {
                if (!cur_target.GetComponent<ICanStealth>().IsStealthy)
                {
                    bullet = Instantiate(bullet_model, BulletPoint.transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().damage = Damage;
                    bullet.GetComponent<Bullet>().target_pos = cur_target.transform.position;
                    OnShoot?.Invoke();
                }
                
            }
            Destroy(bullet, 5);
        }

    }
}
