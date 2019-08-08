using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public class ItemDropBGMEvent : AItemDropEvent, IBGMEvent
    {
        [SerializeField]
        BGMName bGMName;
        [SerializeField]
        AudioCommandType audioCommandType;

        public BGMName BGMName => bGMName;

        public AudioCommandType AudioCommandType => audioCommandType;
    }
}