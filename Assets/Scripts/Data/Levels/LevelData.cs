using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.LevelCreator
{
    [System.Serializable]
    public class LevelData
    {
        public List<List<int>> MatrixIdxOfBlocks;
        //public int[,] MatrixIdxOfBlocksNew; 
        public LevelDesignTypes LevelType;
        public string LevelName;
    }

}
