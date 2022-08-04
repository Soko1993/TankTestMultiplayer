using TanksEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace TanksEngine.LevelCreator
{
    /// <summary>
    /// »спользуетс€ дл€ сохранени€ информации
    /// о созданном уровне.
    /// </summary>
    //[System.Serializable]
    //public class LevelDataFile
    //{
    //    public List<List<int>> MatrixIdxOfBlocks;
    //    public LevelDesignTypes LevelType;
    //}
    /// <summary>
    /// –еализует сборку игровой локации по
    /// опредленным правилам.
    /// </summary>
    public class LevelEditor : MonoBehaviour
    {
        [Header("Game")]
        public string levelName;
        [Header("LevelEditor")]
        public bool EnableEditor;
        public LevelEditorUI levelEditorUI;
        //public int ItemSize = 10;
        public LevelDesignesSO levelDesignVendor;
        public LevelDesignTypes levelType;

        //public int[,] MatrixIdxOfBlocks;
        public List<List<int>> MatrixIdxOfBlocks;
        public int selectedBlockType { get; private set; } = 0;
        public int MapSizeX { get; private set; } = 3;
        public int MapSizeY { get; private set; } = 3;
        public LevelsStorageSO levelsStorage;
        public int currentLvlId { get; private set; } = 0;
        private List<GameObject> objPool = new();
        private Vector3 pos;
        private Vector3 rotation;
        private void Awake()
        {
            SettingLevelId();
            if (levelEditorUI != null)
            {
                levelEditorUI.UpdateEditorUI();
            }

        }
        public void UpdateLevel(int x, int y)
        {
            MapSizeX = x;
            MapSizeY = y;
            MatrixIdxOfBlocks = new(x);

            for (int i = 0; i < x; i++)
            {
                MatrixIdxOfBlocks.Add(new List<int>(y));
            }
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    MatrixIdxOfBlocks[i].Add(0);
                }
            }
            BuildLevel();
            levelEditorUI.UpdateEditorUI();
        }
        public void SetBlock(int i, int j)
        {
            Debug.Log("setblock");
            MatrixIdxOfBlocks[i][j] = selectedBlockType;
            BuildLevel();
        }
        public void SetBlockType(int blockType)
        {
            selectedBlockType = blockType;
        }
        /// <summary>
        /// —охран€ет id выбранного дизайна лелвела (leveltype)
        /// </summary>
        public void SettingLevelId()
        {
            for (int i = 0; i < levelDesignVendor.Levels.Length; i++)
            {
                if (levelDesignVendor.Levels[i].LevelDesignData.LevelType == levelType)
                {
                    currentLvlId = i;
                }
            }
        }
        /// <summary>
        /// —обирает уровень из блоков выбранного дизайна.
        /// ќтдельно собираетс€ внешн€€ часть, отдельно внутренн€€.
        /// </summary>
        private void BuildLevel()
        {
            SettingLevelId();
            ClearLevel();
            int j = 0;
            int n = 0;
            //¬нешн€€ часть
            for (int i = -1; i < MapSizeX + 1; i++)
            {
                for (j = -1; j < MapSizeY + 1; j++)
                {
                    if (i == -1 || i == MapSizeX)
                    {
                        pos.x = i * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                        pos.z = j * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                        objPool.Add(GameObject.Instantiate(GetBlockBorder(), pos, Quaternion.Euler(rotation)));
                    }
                    else
                    {
                        if (j == -1 || j == MapSizeY)
                        {
                            pos.x = i * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                            pos.z = j * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                            objPool.Add(GameObject.Instantiate(GetBlockBorder(), pos, Quaternion.Euler(rotation)));
                        }
                    }
                }
            }
            //¬нутренн€ часть
            for (int i = 0; i < MapSizeX; i++)
            {
                for (j = 0; j < MapSizeY; j++)
                {
                    pos.x = i * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                    pos.z = j * levelDesignVendor.Levels[currentLvlId].LevelDesignData.BlockSize;
                    GameObject obj = (GameObject.Instantiate(levelDesignVendor.Levels[currentLvlId].LevelDesignData.Blocks[MatrixIdxOfBlocks[i][j]], pos, Quaternion.Euler(rotation)));
                    LevelBlock comp = obj.AddComponent<LevelBlock>();
                    if (!EnableEditor)
                    {
                        //obj.GetComponent<BoxCollider>().enabled = false;
                    }
                    comp.x = i;
                    comp.y = j;
                    comp.levelEditorUI = levelEditorUI;
                    objPool.Add(obj);
                    n++;
                }
            }
        }
        public void SetLevelType(LevelDesignTypes val)
        {
            levelType = val;
            UpdateLevel(MapSizeX, MapSizeY);
        }
        public void SaveLevel(string name)
        {
            Debug.Log("Save level " + name);
            LevelData levelData = new LevelData();
            int x = MatrixIdxOfBlocks.Count;
            int y = MatrixIdxOfBlocks[0].Count;
            levelData.MatrixIdxOfBlocks = new(x);
            levelData.LevelType = levelType;
            levelData.LevelName = name;
           // levelData.MatrixIdxOfBlocksNew = new int[x, y];
            for (int i = 0; i < MatrixIdxOfBlocks.Count; i++)
            {
                List<int> tempList = new();
                tempList.Clear();
                for (int j = 0; j < MatrixIdxOfBlocks[0].Count; j++)
                {
                    tempList.Add(MatrixIdxOfBlocks[i][j]);
                }
                var a = tempList;
                levelData.MatrixIdxOfBlocks.Add(a);
            }
            //Directory.CreateDirectory(Application.dataPath + @"\Levels\");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(levelData);
            File.WriteAllText(Application.dataPath + $@"/Resources/Levels2/{name}.txt", json);

        }
        public void LoadLevelFromStorage(TextAsset txtLeveldata)
        {
            Debug.Log("Load level " + name);
            //LevelData levelData = new();

            //foreach (var item in levelsStorage.LevelStorage)
            //{
            //    if (item.levelData.LevelName == name)
            //    {
            //        levelData = item.levelData;
            //        //string path = AssetDatabase.GetAssetPath(item);
            //        //var a = AssetDatabase.LoadAssetAtPath<LevelDataSO>(path);
            //    }
            //}

            LevelData levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(txtLeveldata.text);

            MatrixIdxOfBlocks = new(levelData.MatrixIdxOfBlocks.Count);
            levelType = levelData.LevelType;
            for (int i = 0; i < levelData.MatrixIdxOfBlocks.Count; i++)
            {
                List<int> tempList = new();
                tempList.Clear();
                for (int j = 0; j < levelData.MatrixIdxOfBlocks[0].Count; j++)
                {
                    // MatrixIdxOfBlocks[i].Add(levelData.MatrixIdxOfBlocks[i][j]);
                    tempList.Add(levelData.MatrixIdxOfBlocks[i][j]);
                }
                var a = tempList;
                MatrixIdxOfBlocks.Add(a);
            }
            MapSizeX = MatrixIdxOfBlocks.Count;
            MapSizeY = MatrixIdxOfBlocks[0].Count;
            BuildLevel();
        }
        public void LoadLevel(string name)
        {
            //Debug.Log("Load level " + name);
            //string json = File.ReadAllText(Application.dataPath + $@"\Levels\{name}.txt");
            //LevelData levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(json);
            //MatrixIdxOfBlocks = new int[levelData.MatrixIdxOfBlocks.Count, levelData.MatrixIdxOfBlocks[0].Count];
            //levelType = levelData.LevelType;
            //for (int i = 0; i < levelData.MatrixIdxOfBlocks.Count; i++)
            //{

            //    for (int j = 0; j < levelData.MatrixIdxOfBlocks[0].Count; j++)
            //    {
            //        MatrixIdxOfBlocks[i][ j] = levelData.MatrixIdxOfBlocks[i][j];
            //    }
            //}
            //MapSizeX = MatrixIdxOfBlocks.GetLength(0);
            //MapSizeY = MatrixIdxOfBlocks.GetLength(1);
            BuildLevel();
        }
        private GameObject GetBlockBorder()
        {
            int x = Random.Range(0, levelDesignVendor.Levels[currentLvlId].LevelDesignData.BorderBlocks.Length);

            for (int i = 0; i < levelDesignVendor.Levels[currentLvlId].LevelDesignData.BorderBlocks.Length; i++)
            {
                if (i == x)
                {
                    return levelDesignVendor.Levels[currentLvlId].LevelDesignData.BorderBlocks[i];
                }
            }
            return null;
        }
        private void ClearLevel()
        {
            for (int i = 0; i < objPool.Count; i++)
            {
                GameObject.Destroy(objPool[i]);
            }
            objPool.Clear();
        }
    }
}

