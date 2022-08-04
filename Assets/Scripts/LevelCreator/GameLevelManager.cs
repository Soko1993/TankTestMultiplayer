using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.LevelCreator
{
    /// <summary>
    /// ��������� ��������� ������� �������
    /// </summary>
    public class GameLevelManager : MonoBehaviour
    {
        public LevelEditor levelEditor;
        public bool LoadLevelOnStart;
        public bool EnableEditor;

        public LevelsStorageSO levelsStorage;
        /// <summary>
        /// ��������� ������� �� ��������
        /// </summary>
        /// <param name="levelName">�������� �������</param>
        public void LoadLevelByName(string levelName)
        {
            if (LoadLevelOnStart)
            {
                levelEditor.LoadLevel(levelName);
            }
            if (EnableEditor)
            {
                levelEditor.UpdateLevel(5, 5);
            }
        }
        /// <summary>
        /// ��������� ��������� ������� �� ����� � ���������
        /// </summary>
        public void LoadLevelRandom()
        {
            int randomLvlIdx = Random.Range(0, Resources.LoadAll<TextAsset>("Levels2/").Length);
            int n = 0;
            TextAsset txtLevelData = null;
            foreach (var item in Resources.LoadAll<TextAsset>("Levels2/"))
            {
                if (n == randomLvlIdx)
                {
                    txtLevelData = item;
                }
                n++;
            }
            if (txtLevelData != null)
            {

                levelEditor.LoadLevelFromStorage(txtLevelData);
            }
            else
            {
                Debug.LogError("Can't load level");
            }

            if (EnableEditor)
            {
                levelEditor.UpdateLevel(5, 5);
            }
        }

        

    }
}

