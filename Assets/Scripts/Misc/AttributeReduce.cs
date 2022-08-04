using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeReduce : MonoBehaviour
{
    public float Damage;
    public GameObject target;
    [ContextMenu("ReduceHP")]
    public void HpReduce()
    {
        CharacterEntity char_attr;

        char_attr = target.GetComponent<CharacterEntity>();
        
        if (char_attr != null) 
        {   
            target.GetComponent<CharacterEntity>().ChangeCurHP(Damage); 
        }
        
    }
}
