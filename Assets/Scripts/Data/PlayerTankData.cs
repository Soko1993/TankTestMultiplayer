using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.Data
{
    /// <summary>
    /// Используется как слот ангара.
    /// </summary>
    [System.Serializable]
    public struct PlayerTankData
    {
        //Что содержит данный слот
        public AngarSlotType slotType;

        //Доступные детали для данного типа танка
        public List<Tank_Detail> detailMainList;
        public List<Tank_Detail> detailTurretList;
        public List<Tank_Detail> detailEngineList;
        public List<Tank_Detail> detailSuspensionList;

        //Доступные командиры
        public List<Commander> commanderList;

        //Структура с текущими деталями на танке
        public TankDataStruct tankDataStruct;

        //Шаблон детали
        [System.Serializable]
        public struct Tank_Detail
        {
            public string Name;
            public Sprite Icon;
            public Mesh Detail_mesh;
            public Material Detail_material;
        }
        //Шаблон командира
        [System.Serializable]
        public struct Commander
        {
            public string Name;
            public Sprite Icon;
        }
    }


}
