using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class ItemData
    {
        public string Name;
        public int Score;
        public int Weight;

        public ItemData(string name, int score, int weight)
        {
            Name = name;
            Score = score;
            Weight = weight;
        }
    }
}