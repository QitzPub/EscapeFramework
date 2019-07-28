using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework {

    public interface IItemSpriteVO
    {
        ItemName ItemName { get; }
        Sprite Sprite { get; }
    }

    [System.Serializable]
    public class ItemSpriteVO: IItemSpriteVO
    {
        [SerializeField]
        ItemName itemName;
        public ItemName ItemName => itemName;
        [SerializeField]
        Sprite sprite;
        public Sprite Sprite => sprite;
    }
}
