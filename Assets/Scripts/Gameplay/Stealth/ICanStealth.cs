using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������� ������� ���� ��������
/// </summary>
public interface ICanStealth 
{
    public bool IsStealthy { get; set; }
    public void EnableStealt();
    public void DisableStealt();
}
