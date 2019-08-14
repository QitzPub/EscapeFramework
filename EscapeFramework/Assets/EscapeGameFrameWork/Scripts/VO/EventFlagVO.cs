using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IEventFlagVO
    {
        EventName EventType { get; }
        bool IsOn { get; }
    }

    [System.Serializable]
    public class EventFlagVO: IEventFlagVO
    {

        [SerializeField]
        int eventType;
        [SerializeField]
        bool isOn;

        public EventName EventType => (EventName)eventType;
        public bool IsOn => isOn;

        public EventFlagVO(EventName eventType,bool isOn)
        {
            this.eventType = (int)eventType;
            this.isOn = isOn;
        }

    }
}