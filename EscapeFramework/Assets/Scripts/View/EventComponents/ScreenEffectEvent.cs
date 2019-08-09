using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{

    public interface IScreenEffectEvent
    {
        ScreenEffectName ScreenEffect { get; }
    }

    public class ScreenEffectEvent : AEscapeGameEvent, IScreenEffectEvent
    {
        [SerializeField]
        ScreenEffectName screenEffectName;
        public ScreenEffectName ScreenEffect => screenEffectName;

    }
}