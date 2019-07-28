using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class EventFlagEventView : AEventViewBase
    {
        [SerializeField]
        EventType eventType;
        public EventType EventType => eventType;
        [SerializeField]
        EventFlag eventFlag;
        public EventFlag EventFlag => eventFlag;
    }
}
