using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public interface IADVWindowEvent
    {
        List<string> Texts { get; }
    }

    public class ADVWindowEvent : AEscapeGameEvent, IADVWindowEvent
    {
        [SerializeField]
        List<string> texts;
        public List<string> Texts => texts;
    }
}