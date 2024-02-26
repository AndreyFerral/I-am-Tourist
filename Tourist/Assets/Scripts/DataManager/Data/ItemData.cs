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
        public string TextItem;
        public float WeightItem;

        public ItemData(string path, string file, string text, float weight)
        {
            PathPicture = path;
            FileName = file;
            TextItem = text;
            WeightItem = weight;
        }
    }
}