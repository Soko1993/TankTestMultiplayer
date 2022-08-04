using TanksEngine.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TanksEngine.UI 
{
    public static class MenuTankDisplayer
    {
        /// <summary>
        /// Реализует обновление деталей
        /// на выбранной модели танка.
        /// </summary>
        public static void UpdateTankVisual(int angarSlot, GameObject playerModel)
        {
            var gameData = GameData.AngarSlots[GameData.CurSlot].tankDataStruct;
            var playerData = playerModel.GetComponent<TankDetailsManager>();

            playerData.ModelDetailMain.GetComponent<MeshFilter>().mesh = gameData.detailMain.Detail_mesh;
            playerData.ModelDetailMain.GetComponent<MeshRenderer>().material = gameData.detailMain.Detail_material;

            playerData.ModelDetailEngine.GetComponent<MeshFilter>().mesh = gameData.detailEngine.Detail_mesh;
            playerData.ModelDetailEngine.GetComponent<MeshRenderer>().material = gameData.detailEngine.Detail_material;

            playerData.ModelDetailSuspension.GetComponent<MeshFilter>().mesh = gameData.detailSuspension.Detail_mesh;
            playerData.ModelDetailSuspension.GetComponent<MeshRenderer>().material = gameData.detailSuspension.Detail_material;

            playerData.ModelDetailTurret.GetComponent<MeshFilter>().mesh = gameData.detailTurret.Detail_mesh;
            playerData.ModelDetailTurret.GetComponent<MeshRenderer>().material = gameData.detailTurret.Detail_material;
        }
        public static void ClearTankViusal(GameObject playerModel)
        {
            var playerData = playerModel.GetComponent<TankDetailsManager>();
            playerData.ModelDetailMain.GetComponent<MeshFilter>().mesh = null;
            playerData.ModelDetailMain.GetComponent<MeshRenderer>().material = null;

            playerData.ModelDetailEngine.GetComponent<MeshFilter>().mesh = null;
            playerData.ModelDetailEngine.GetComponent<MeshRenderer>().material = null;

            playerData.ModelDetailSuspension.GetComponent<MeshFilter>().mesh = null;
            playerData.ModelDetailSuspension.GetComponent<MeshRenderer>().material = null;

            playerData.ModelDetailTurret.GetComponent<MeshFilter>().mesh = null;
            playerData.ModelDetailTurret.GetComponent<MeshRenderer>().material = null;
        }

    }
}

