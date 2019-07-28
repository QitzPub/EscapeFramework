using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class EventFlagIGnitionPointSetting : AIgnitionPointBase
    {
        [SerializeField]
        EventType eventType;
        [SerializeField]
        EventFlag eventFlag;
        public EventType EventType => eventType;
        public EventFlag EventFlag => eventFlag;
    }
}
