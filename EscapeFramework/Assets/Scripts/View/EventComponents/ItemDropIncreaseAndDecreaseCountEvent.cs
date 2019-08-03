using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IIncreaseAndDecreaseCountEvent
    {
        CountEventType CountEventName { get; }
        EventProgress EventProgress { get; }
        CountEventVO CountEventVO { get; }
        GameObject gameObject { get; }
    }
    public class ItemDropIncreaseAndDecreaseCountEvent : AItemDropEvent, IIncreaseAndDecreaseCountEvent
    {
        [SerializeField, HeaderAttribute("以下カウントイベントを")]
        CountEventType countEventName;
        [SerializeField]
        EventProgress eventProgress;

        public CountEventType CountEventName => countEventName;
        public EventProgress EventProgress => eventProgress;
        public CountEventVO CountEventVO => new CountEventVO(countEventName);
    }
}
