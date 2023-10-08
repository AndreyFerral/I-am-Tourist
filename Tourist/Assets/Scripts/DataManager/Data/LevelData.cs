using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class LevelData
    {
        public string Name;
        public int Score;

        public LevelData(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}