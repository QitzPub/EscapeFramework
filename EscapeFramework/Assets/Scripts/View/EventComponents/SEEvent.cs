using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface ISEEvent
    {
        SEName SEName { get; }
        AudioCommandType AudioCommandType { get; }
    }

    public class SEEvent : AEscapeGameEvent, ISEEvent
    {
        [SerializeField]
        SEName sEName;
        [SerializeField]
        AudioCommandType audioCommandType;

        public SEName SEName => sEName;
        public AudioCommandType AudioCommandType => audioCommandType;
    }
}
