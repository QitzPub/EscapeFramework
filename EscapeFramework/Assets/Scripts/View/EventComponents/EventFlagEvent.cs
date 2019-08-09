using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEventFlagEvent
    {
        EventName EventType { get; }
        EventFlag EventFlag { get; }
        EventFlagVO EventFlagVO { get; }
        GameObject gameObject { get; }
    }

    public class EventFlagEvent : AEscapeGameEvent, IEventFlagEvent
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
