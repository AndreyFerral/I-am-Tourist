using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class EventsInfoData
    {
        public string EventName;
        public int EventInfoId;
        public string PathEventInfoPicture;
        public string TextButtonBefore;
        public string TextButtonAfter;
        public string ObjectReceiverSignal;

        public EventsInfoData(string event_name, int event_info_id, string event_path, string button_before, string button_after, string object_receiver)
        {
            EventName = event_name;
            EventInfoId = event_info_id;
            PathEventInfoPicture = event_path;
            TextButtonBefore = button_before;
            TextButtonAfter = button_after;
            ObjectReceiverSignal = object_receiver;
        }
    }
}