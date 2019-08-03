using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemDropIncreaseItemEvent : AItemDropEvent, IIncreaseAndDecreaseItemEvent
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
