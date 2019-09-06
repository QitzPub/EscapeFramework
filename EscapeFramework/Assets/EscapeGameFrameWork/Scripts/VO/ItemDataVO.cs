using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework {

    public interface IItemDataVO
    {
        ItemName ItemName { get; }
        Sprite Sprite { get; }
        string ItemDescription { get; }
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
        public string ItemDescription => itemDescription;
    }
}
