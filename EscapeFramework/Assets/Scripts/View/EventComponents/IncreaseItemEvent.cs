using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IIncreaseItemEvent
    {
        ItemName ItemName { get; }
        ItemEventProgress ItemEventProgress { get; }
        ItemVO ItemVO { get; }
        AIgnitionPointBase AIgnitionPointBase { get; }
        GameObject gameObject { get; }
    }
    public class IncreaseItemEvent : AEscapeGameEvent, IIncreaseItemEvent
    {
        [SerializeField, HeaderAttribute("以下アイテムを")]
        ItemName itemName;
        [SerializeField]
        ItemEventProgress itemEventProgress;

        public ItemName ItemName => itemName;
        public ItemEventProgress ItemEventProgress => itemEventProgress;
        public ItemVO ItemVO => new ItemVO(itemName);
    }
}
