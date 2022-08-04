using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TanksEngine.LevelCreator 
{
    //OLD
    /// <summary>
    /// –еализует сборку игровой локации по
    /// опредленным правилам.
    /// </summary>
    public class LevelCreator : MonoBehaviour
    {
        public bool GenerateRandom;

        public int MapSizeX = 3;
        public int MapSizeY = 3;
        public int ItemSize = 10;
        
        public GameObject[] blocks;
        public GameObject[] borderBlocks;

        public int[,] MatrixIdxOfBlocks;

        private List<GameObject> objPool=new();
        private Vector3 pos;
        private Vector3 rotation;

        public void LoadLevel(string name)
        {
            Debug.Log("Load level " + name);
            string json = File.ReadAllText(Application.dataPath + $@"\Levels\{name}.txt");
            LevelData levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(json);
            MatrixIdxOfBlocks = new int[levelData.MatrixIdxOfBlocks.Count, levelData.MatrixIdxOfBlocks[0].Count];

            for (int i = 0; i < levelData.MatrixIdxOfBlocks.Count; i++)
            {

                for (int j = 0; j < levelData.MatrixIdxOfBlocks[0].Count; j++)
                {
                    MatrixIdxOfBlocks[i, j] = levelData.MatrixIdxOfBlocks[i][j];
                }
            }
            MapSizeX = MatrixIdxOfBlocks.GetLength(0);
            MapSizeY = MatrixIdxOfBlocks.GetLength(1);
            BuildLevel();
        }

        void Start()
        {
            if (!GenerateRandom)
            {
                LoadLevel("level-x");
            }
            BuildLevel();
        }
        [ContextMenu("BuildLevel")]
        public void BuildLevel()
        {
            
            int j = 0;
            int n = 0;
            ClearLevel();
            //¬нешн€€ часть
            for (int i = -1; i < MapSizeX + 1; i++)
            {

                for (j = -1; j < MapSizeY + 1; j++)
                {
                    if(i==-1 || i == MapSizeX)
                    {
                        pos.x = i * ItemSize;
                        pos.z = j * ItemSize;
                        objPool.Add(GameObject.Instantiate(GetBlockBorder(), pos, Quaternion.Euler(rotation)));
                    }
                    else
                    {
                        if (j == -1 || j == MapSizeY)
                        {
                            pos.x = i * ItemSize;
                            pos.z = j * ItemSize;
                            objPool.Add(GameObject.Instantiate(GetBlockBorder(), pos, Quaternion.Euler(rotation)));
                        }
                    }
                    
                    
                }
            }
            //¬нутренн€ часть
            for (int i = 0; i < MapSizeX; i++)
            {
                for ( j = 0; j < MapSizeY; j++)
                {
                    pos.x = i * ItemSize;
                    pos.z = j * ItemSize;
                    if (GenerateRandom)
                    {
                        objPool.Add(GameObject.Instantiate(GetBlock(), pos, Quaternion.Euler(rotation)));
                    }
                    else
                    {
                        objPool.Add(GameObject.Instantiate(blocks[MatrixIdxOfBlocks[i,j]], pos, Quaternion.Euler(rotation)));
                    }
                    n++;
                }
            }
        }
        private GameObject GetBlock()
        {
            int x = Random.Range(0, blocks.Length);

            for (int i = 0; i < blocks.Length; i++)
            {
                if (i == x)
                {
                    return blocks[i];
                }
                
            }
            return null;
        }
        private GameObject GetBlockBorder()
        {
            int x = Random.Range(0, borderBlocks.Length);

            for (int i = 0; i < borderBlocks.Length; i++)
            {
                if (i == x)
                {
                    return borderBlocks[i];
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
        }
    }
}

