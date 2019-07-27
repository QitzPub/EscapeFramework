using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework {

    public interface IItemVO
    {
        ItemName ItemName { get; }
        Sprite Sprite { get; }
    }

    [System.Serializable]
    public class ItemVO: IItemVO
    {
        [SerializeField]
        ItemName itemName;
        public ItemName ItemName => itemName;
        [SerializeField]
        Sprite sprite;
        public Sprite Sprite => sprite;
    }
}
