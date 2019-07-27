using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public enum ItemPossession
    {
        持っている,
        持っていない,
    }
    
    public class ItemEvent : MonoBehaviour
    {
        [SerializeField]
        ItemName itemName;
        [SerializeField, HeaderAttribute("を")]
        ItemPossession itemPossession;
        [SerializeField, HeaderAttribute("の時")]
        EventProgress eventProgress;
    }
}