using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemDropEventFlagEvent : AItemDropEvent, IEventFlagEvent
    {
        [SerializeField]
        EventName eventType;
        public EventName EventType => eventType;
        [SerializeField]
        EventFlag eventFlag;
        public EventFlag EventFlag => eventFlag;
        public EventFlagVO EventFlagVO => new EventFlagVO(EventType, EventFlag == EventFlag.ON);
    }
}
