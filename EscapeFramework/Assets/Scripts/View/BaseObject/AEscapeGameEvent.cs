using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public abstract class AEscapeGameEvent : AEvent
    {
        [SerializeField]
        EventExecuteTiming eventExecuteTiming;
        public EventExecuteTiming EventExecuteTiming => eventExecuteTiming;
    }
}