using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackSystem
{
    private static DamagesDataSO damagesDataSO;
    public static void AttackTarget(GameObject target, DamageType damageType)
    {
        ParticleSystem _particle = null;
        foreach (var item in damagesDataSO.DamageDatas)
        {
            if (item.DamageType == damageType)
            {
                _particle = item.HitEffect;
            }
        }
        target.GetComponent<ICanTakeEffect>().PlayEffect(_particle);
    }

}
