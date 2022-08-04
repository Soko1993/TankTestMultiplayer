using TanksEngine.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Реализует сущность персонажа
/// Это может быть как игрок, так и Npc
/// </summary>
public class CharacterEntity : MonoBehaviour
{
    #region[Variables]
    [Header("Settings")]
    public bool IsPlayer;
    
    [Header("Death")]
    public bool DestroyOnDeath;
    public float DestroyDelay;
    [Header("----------------")]
    public bool PlayAnimOnDeath;
    public string AnimTriggerOnDeath;
    [Header("----------------")]
    public bool PlaySoundOnDeath;
    public AudioClip AudioClipDeath;
    public AudioSource CharacterAudioSrc;
    [Header("----------------")]
    public bool PlayEffectsOnDeath;
    public ParticleSystem DeathParticle;

    [Header("Events")]
    public UnityEvent OnDeath;
    public UnityEvent OnChangeHP;

    public float Cur_HP { get; private set; }
    public float Hp_Max { get; private set; }
    public float Speed { get; private set; }
    public float Damage { get; private set; }
    public bool IsAlive { get; private set; }

    private TankDetailsManager tankdetails;
    #endregion
    public void Start()
    {
        tankdetails = GetComponent<TankDetailsManager>();
        Invoke(nameof(UpdateAttributes), 0.2f);
        
    }
    /// <summary>
    /// Обновляет аттрибуты
    /// </summary>
    public void UpdateAttributes() 
    {
        Hp_Max = 40;
        Cur_HP = Hp_Max;
        Speed = 10;
        Damage = 5;
        IsAlive = Cur_HP > 0;
        if (IsPlayer)
        {
            //OnChangeAttributes?.Invoke();
        }
        
    }
    /// <summary>
    /// Изменяет максимальное количество ХП
    /// </summary>
    /// <param name="amount">Кол-во</param>
    /// <param name="fillHp">Восполнить до максиума</param>
    public void ChangeMaxHP(float amount,bool fillHp)
    {
        OnChangeHP?.Invoke();
        if (amount > 0)
        {
            Hp_Max = amount;
            if (fillHp)
            {
                Cur_HP += Hp_Max - Cur_HP;
            }
        }
        if (IsPlayer)
        {
            Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Health);
        }
    }
    /// <summary>
    /// Изменяет текущее ХП
    /// </summary>
    /// <param name="amount">Кол-во</param>
    public void ChangeCurHP(float amount) 
    {
        OnChangeHP?.Invoke();
        if (Cur_HP > 0)
        {
            if((Cur_HP <= Hp_Max))
            {
                Cur_HP += amount;

                if (Cur_HP > Hp_Max) { Cur_HP -= Cur_HP - Hp_Max; }
                if (Cur_HP <=0 ) { Death(); }

                if (IsPlayer)
                {
                    Global_EventManager.CallOnAttributeUpdate(AttributeTypes.Health);
                }
            }
            else
            {
                Debug.Log("Cur_HP >= Hp_Max");
            }
            
        }
        else
        {
            Debug.Log("Cur_HP <= 0");
        }
    }
    /// <summary>
    /// Обрабатывает смерть персонажа
    /// </summary>
    private void Death() 
    {
        OnDeath?.Invoke();
        if (IsPlayer)
        {
            Global_EventManager.CallOnPlayerDie();
        }
        else
        {
            Global_EventManager.CallOnEnemyDie(gameObject);
        }
        
        Debug.Log($"{gameObject.name} is died");

        IsAlive = false;

        if (DestroyOnDeath)
        {
            Destroy(gameObject, DestroyDelay);
        }
        
    }
}
