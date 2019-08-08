using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public interface IBGMEvent
    {
        BGMName BGMName { get; }
        AudioCommandType AudioCommandType { get; }
    }

    public class BGMEvent : AEscapeGameEvent, IBGMEvent
    {
        [SerializeField]
        BGMName bGMName;
        [SerializeField]
        AudioCommandType audioCommandType;

        public BGMName BGMName => bGMName;

        public AudioCommandType AudioCommandType => audioCommandType;
    }
}