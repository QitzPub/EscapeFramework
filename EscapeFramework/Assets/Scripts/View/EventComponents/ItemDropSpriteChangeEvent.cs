using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemDropSpriteChangeEvent : AItemDropEvent, ISpriteChangeEvent
    {
        [SerializeField]
        Sprite changeSprite;
        public Sprite ChangeSprite => changeSprite;
    }
}
