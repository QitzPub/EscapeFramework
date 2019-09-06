using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework {

    public interface IItemDataVO
    {
        ItemName ItemName { get; }
        Sprite Sprite { get; }
        string ItemDescription { get; }
        ItemCompostionInfo[] ItemCompostionInfos { get; }
        ItemName JugSynthesizeable(ItemName itemA, ItemName itemB);
    }
    [System.Serializable]
    public class ItemCompostionInfo
    {
        [SerializeField]
        ItemName itemA;
        public ItemName ItemA => itemA;
        [SerializeField]
        ItemName itemB;
        public ItemName ItemB => itemB;

        public bool JugSynthesizeable(ItemName itemA, ItemName itemB)
        {
            List<ItemName> allItem = new List<ItemName>() {this.itemA,this.itemB};
            return allItem.Contains(itemA) && allItem.Contains(itemB);
        }

    }

    [System.Serializable]
    public class ItemDataVO: IItemDataVO
    {
        [SerializeField]
        ItemName itemName;
        public ItemName ItemName => itemName;

        [SerializeField]
        Sprite sprite;
        public Sprite Sprite => sprite;

        [SerializeField]
        string itemDescription;

        [SerializeField]
        ItemCompostionInfo[] itemCompostionInfos;
        public ItemCompostionInfo[] ItemCompostionInfos => itemCompostionInfos;

        public string ItemDescription => itemDescription;
        public ItemName JugSynthesizeable(ItemName itemA, ItemName itemB)
        {
            foreach (var ic in itemCompostionInfos)
            {
                bool synthesizeable=ic.JugSynthesizeable(itemA, itemB);
                if (synthesizeable)
                {
                    return itemName;
                }
            }
            return ItemName.NONE;
        }

    }
}
