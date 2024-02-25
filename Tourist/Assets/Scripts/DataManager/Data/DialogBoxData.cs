using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class DialogBoxData
    {
        public string TagName;
        public string TextBefore;
        public string TextAfter;

        public DialogBoxData(string object_name, string text_before, string text_after)
        {
            TagName = object_name;
            TextBefore = text_before;
            TextAfter = text_after;
        }
    }
}