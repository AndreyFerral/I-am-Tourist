using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataNamespace
{
    [System.Serializable]
    public class EventsItemsData
    {
        public string EventName;
        public int EventInfoId;

        public List<string> ItemsToUse;
        public List<string> ItemsToDelete;
        public List<string> ItemsToCreate;

        public int ValueStamina;

        public EventsItemsData(string event_name, int event_info_id, List<string> items_use, List<string> items_del, List<string> items_create, int value)
        {
            EventName = event_name;
            EventInfoId = event_info_id;
            ItemsToUse = items_use;
            ItemsToDelete = items_del;
            ItemsToCreate = items_create;
            ValueStamina = value;
        }
    }
}