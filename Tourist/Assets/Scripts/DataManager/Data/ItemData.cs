using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class ItemData
    {
        public string PathPicture;
        public string FileName;
        public string VisibleName;
        public float WeightItem;

        public ItemData(string path, string file, string name, float weight)
        {
            PathPicture = path;
            FileName = file;
            VisibleName = name;
            WeightItem = weight;
        }
    }
}