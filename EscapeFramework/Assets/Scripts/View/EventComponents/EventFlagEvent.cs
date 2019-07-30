using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEventFlagEvent
    {
        EventType EventType { get; }
        EventFlag EventFlag { get; }
        EventFlagVO EventFlagVO { get; }
        GameObject gameObject { get; }
    }

    public class EventFlagEvent : AEscapeGameEvent, IEventFlagEvent
    {
        [SerializeField]
        EventType eventType;
        public EventType EventType => eventType;
        [SerializeField]
        EventFlag eventFlag;
        public EventFlag EventFlag => eventFlag;
        public EventFlagVO EventFlagVO => new EventFlagVO(EventType, EventFlag == EventFlag.ON);
    }
}
