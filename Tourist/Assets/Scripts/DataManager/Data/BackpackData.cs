using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class BackpackData
    {
        public int IdBackpack;
        public string NameBackpack;
        public float Stamina;

        public BackpackData(int id, string name, float value)
        {
            IdBackpack = id;
            NameBackpack = name;
            Stamina = value;
        }
    }
}