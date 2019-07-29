using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(ItemDropable))]
    public abstract class AItemDropEvent : AEvent
    {
        [SerializeField, HeaderAttribute("このアイテムがドロップされたときに")]
        ItemName dropedItemName;
        public ItemName DropedItemName => dropedItemName;
        public ItemDropable DropableView => this.GetComponent<ItemDropable>();
    }
}
