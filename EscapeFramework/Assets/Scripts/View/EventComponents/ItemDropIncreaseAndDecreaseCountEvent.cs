using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IIncreaseAndDecreaseCountEvent
    {
        CountEventType CountEventName { get; }
        CountEventProgress CountEventProgress { get; }
        CountEventVO CountEventVO { get; }
        GameObject gameObject { get; }
    }
    public class ItemDropIncreaseAndDecreaseCountEvent : AItemDropEvent, IIncreaseAndDecreaseCountEvent
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
