using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemDropSEEvent : AItemDropEvent, ISEEvent
    {
        [SerializeField]
        SEName sEName;
        [SerializeField]
        AudioCommandType audioCommandType;

        public SEName SEName => sEName;
        public AudioCommandType AudioCommandType => audioCommandType;
    }
}