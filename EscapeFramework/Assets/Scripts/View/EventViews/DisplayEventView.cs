using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class DisplayEventView : AEventViewBase
    {
        [SerializeField, HeaderAttribute("アイテムを表示-非表示切り替え")]
        DisplayEventProgress displayEventProgress;
        public DisplayEventProgress DisplayEventProgress => displayEventProgress;
    }
}

