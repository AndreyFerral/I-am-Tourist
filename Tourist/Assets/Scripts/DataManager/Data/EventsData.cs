using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class EventsData
    {
        public string EventName;
        public string VisibleName;
        public string PathEventPicture;

        public EventsData(string event_name, string visible_name, string event_path)
        {
            EventName = event_name;
            VisibleName = visible_name;
            PathEventPicture = event_path;
        }
    }
}