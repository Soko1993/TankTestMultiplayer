using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Data
{
    /// <summary>
    /// ������������ ��� ���� ������.
    /// </summary>
    [System.Serializable]
    public struct PlayerTankData
    {
        //��� �������� ������ ����
        public AngarSlotType slotType;

        //��������� ������ ��� ������� ���� �����
        public List<Tank_Detail> detailMainList;
        public List<Tank_Detail> detailTurretList;
        public List<Tank_Detail> detailEngineList;
        public List<Tank_Detail> detailSuspensionList;

        //��������� ���������
        public List<Commander> commanderList;

        //��������� � �������� �������� �� �����
        public TankDataStruct tankDataStruct;

        //������ ������
        [System.Serializable]
        public struct Tank_Detail
        {
            public string Name;
            public Sprite Icon;
            public Mesh Detail_mesh;
            public Material Detail_material;
        }
        //������ ���������
        [System.Serializable]
        public struct Commander
        {
            public string Name;
            public Sprite Icon;
        }
    }


}
