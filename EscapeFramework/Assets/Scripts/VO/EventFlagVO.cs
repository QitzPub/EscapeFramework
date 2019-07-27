using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEventFlagVO
    {
        EventType EventType { get; }
        bool IsOn { get; }
    }

    [System.Serializable]
    public class EventFlagVO: IEventFlagVO
    {

        [SerializeField]
        EventType eventType;
        [SerializeField]
        bool isOn;

        public EventType EventType => eventType;
        public bool IsOn => isOn;

        public EventFlagVO(EventType eventType,bool isOn)
        {
            this.eventType = eventType;
            this.isOn = isOn;
        }

    }
}