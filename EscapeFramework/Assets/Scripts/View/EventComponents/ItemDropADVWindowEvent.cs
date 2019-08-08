using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public class ItemDropADVWindowEvent : AItemDropEvent, IADVWindowEvent
    {
        [SerializeField]
        List<string> texts;
        public List<string> Texts => texts;
    }
}
