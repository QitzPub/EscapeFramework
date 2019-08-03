using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public interface ISpriteChangeEvent
    {
        Sprite ChangeSprite { get; }
        GameObject gameObject { get; }
    }
    public class SpriteChangeEvent : AEscapeGameEvent, ISpriteChangeEvent
    {
        [SerializeField]
        Sprite changeSprite;
        public Sprite ChangeSprite => changeSprite;
    }
}
