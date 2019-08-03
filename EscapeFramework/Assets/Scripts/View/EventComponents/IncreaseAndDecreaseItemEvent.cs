using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IIncreaseAndDecreaseItemEvent
    {
        ItemName ItemName { get; }
        EventProgress ItemEventProgress { get; }
        ItemVO ItemVO { get; }
        GameObject gameObject { get; }
    }
    public class IncreaseAndDecreaseItemEvent : AEscapeGameEvent, IIncreaseAndDecreaseItemEvent
    {
        [SerializeField, HeaderAttribute("以下アイテムを")]
        ItemName itemName;
        [SerializeField]
        EventProgress itemEventProgress;

        public ItemName ItemName => itemName;
        public EventProgress ItemEventProgress => itemEventProgress;
        public ItemVO ItemVO => new ItemVO(itemName);
    }
}
