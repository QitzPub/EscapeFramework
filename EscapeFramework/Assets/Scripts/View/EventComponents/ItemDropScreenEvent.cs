using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemDropScreenEvent : AItemDropEvent, IScreenEffectEvent
    {
        [SerializeField]
        ScreenEffectName screenEffectName;
        public ScreenEffectName ScreenEffect => screenEffectName;
    }
}