using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IDisplayEvent
    {
        DisplayEventProgress DisplayEventProgress { get; }
        AIgnitionPointBase AIgnitionPointBase { get; }
        GameObject gameObject { get; }
    }
    public class DisplayEvent : AEscapeGameEvent, IDisplayEvent
    {
        [SerializeField, HeaderAttribute("アイテムを表示-非表示切り替え")]
        DisplayEventProgress displayEventProgress;
        public DisplayEventProgress DisplayEventProgress => displayEventProgress;
    }
}

