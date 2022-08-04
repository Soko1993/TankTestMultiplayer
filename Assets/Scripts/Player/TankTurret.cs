using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��������� ���������� ������ �����:
/// ������������ � ��������
/// </summary>
public class TankTurret : MonoBehaviour
{
    public PlayerCombat player;
    public float range = 30;
    public float TurretSpeed = 60f;//����� ����� �� so
    public float ShotFrequency = 0.2f;//����� ����� �� so
    public bool AimingDone;
    public UnityEvent OnShoot;

    private RaycastHit hit;
    private Vector3 direction;
    private Vector3 idledirection;
    private float _timer;

    private float singleStep;

    private Vector3 targetDirection;
    private Vector3 newDirection;
    /// <summary>
    /// ������������ ������������
    /// </summary>
    public void Aiming()
    {
        idledirection = player.WeaponIdle.transform.position;
        direction = player.TurretPoint.transform.forward;
        //���� ��� ����� ���� -> ������ �������
        if (Physics.Raycast(player.TurretPoint.transform.position, player.TurretPoint.transform.forward * range, out hit, range, 9))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Shoot();
                AimingDone = true;
            }
        }
        else
        {
            AimingDone = false;
        }
        //���� ���� ���� -> ������������ ����� � � ������� !player._Controller.isMoving
        if (player.cur_target != null )
        {
            singleStep = TurretSpeed * Time.deltaTime;
            targetDirection = player.cur_target.transform.position - player.TurretPoint.transform.position;
            newDirection = Vector3.RotateTowards(player.TurretPoint.transform.forward, targetDirection, singleStep, 0.0f);
            player.TurretPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);
        }
        else
        {
            singleStep = TurretSpeed * Time.deltaTime;
            targetDirection = player.WeaponIdle.transform.position - player.TurretPoint.transform.position;
            newDirection = Vector3.RotateTowards(player.TurretPoint.transform.forward, targetDirection, singleStep, 0.0f);
            player.TurretPoint.transform.rotation = new Quaternion(0, Quaternion.LookRotation(newDirection).y, 0, Quaternion.LookRotation(newDirection).w);
        }
    }
    /// <summary>
    /// ��������� �������
    /// </summary>
    private void Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > ShotFrequency)
        {
            _timer -= ShotFrequency;
            if (player.cur_target != null & !player._Controller.isMoving)
            {
                player.bullet = Instantiate(player.bullet_model, player.Weapon.transform.position, Quaternion.identity);
                player.bullet.GetComponent<Bullet>().damage = player._pAttr.Damage;
                player.bullet.GetComponent<Bullet>().target_pos = player.cur_target.transform.position;
                Destroy(player.bullet, 5);
                OnShoot?.Invoke();
            }
            
        }

    }
}
