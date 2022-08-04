using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Описывает объект, который позволяет скрыться
/// </summary>
public class StealthObject : MonoBehaviour
{
    public Material StealthMaterial;
    public Material StandartMaterial;
    public GameObject TargetObject;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TargetObject.GetComponent<MeshRenderer>().material = StealthMaterial;
            other.GetComponent<ICanStealth>().EnableStealt();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        TargetObject.GetComponent<MeshRenderer>().material = StealthMaterial;
    //        other.GetComponent<ICanStealth>().EnableStealt();
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TargetObject.GetComponent<MeshRenderer>().material = StandartMaterial;
            other.GetComponent<ICanStealth>().DisableStealt();
        }
    }
}
