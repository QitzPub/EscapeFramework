using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework {

    public enum EventFlag
    {
        ON,
        OFF,
    }
    public enum EventProgress
    {
        表示する,
        非表示にする,
    }

    public class SwitchEvent : MonoBehaviour
    {
        [SerializeField]
        EventType eventType;
        [SerializeField, HeaderAttribute("が")]
        EventFlag eventFlag;
        [SerializeField, HeaderAttribute("の時")]
        EventProgress eventProgress;

    }
}
