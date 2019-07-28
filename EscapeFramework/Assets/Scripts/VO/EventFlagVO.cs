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
        int eventType;
        [SerializeField]
        bool isOn;

        public EventType EventType => (EventType)eventType;
        public bool IsOn => isOn;

        public EventFlagVO(EventType eventType,bool isOn)
        {
            this.eventType = (int)eventType;
            this.isOn = isOn;
        }

    }
}