using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{

    public class IncreaseAndDecreaseCountEvent : AEscapeGameEvent,IIncreaseAndDecreaseCountEvent
    {
        [SerializeField, HeaderAttribute("以下カウントイベントを")]
        CountEventType countEventName;
        [SerializeField]
        CountEventProgress countEventProgress;

        public CountEventType CountEventName => countEventName;
        public CountEventProgress CountEventProgress => countEventProgress;
        public CountEventVO CountEventVO => new CountEventVO(countEventName);
    }
}
