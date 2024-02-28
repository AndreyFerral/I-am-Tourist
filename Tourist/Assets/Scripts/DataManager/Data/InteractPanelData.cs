using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class InteractPanelData
    {
        public string TagName;
        public string VisibleName;
        public string TextPositive;
        public string TextNegative;

        public InteractPanelData(string object_name, string visible_name, string text_positive, string text_negative)
        {
            TagName = object_name;
            VisibleName = visible_name;
            TextPositive = text_positive;
            TextNegative = text_negative;
        }
    }
}