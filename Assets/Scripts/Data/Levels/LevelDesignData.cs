using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksEngine.Data
{
    [System.Serializable]
    public struct LevelDesignData
    {
        public LevelDesignTypes LevelType;
        public int BlockSize;
        public GameObject[] Blocks;
        public GameObject[] BorderBlocks;
    }

}
