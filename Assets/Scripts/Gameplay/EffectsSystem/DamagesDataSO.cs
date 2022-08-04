using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New DamagesData", menuName = "GameData/DamagesData")]
[System.Serializable]
public class DamagesDataSO : ScriptableObject
{
    public DamageData[] DamageDatas;
}
[System.Serializable]
public struct DamageData
{
    public DamageType DamageType;
    public GameObject Projectile;
    public ParticleSystem HitEffect;
    public float BaseDamage;
    public float BaseSpeed;
}
