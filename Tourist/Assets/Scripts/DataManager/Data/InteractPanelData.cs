using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class InteractPanelData
    {
        public string TagName;
        public string TextName;
        public string TextPositive;
        public string TextNegative;

        public InteractPanelData(string object_name, string text_name, string text_positive, string text_negative)
        {
            TagName = object_name;
            TextName = text_name;
            TextPositive = text_positive;
            TextNegative = text_negative;
        }
    }
}