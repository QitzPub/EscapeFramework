using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class ItemIGnitionPointSetting : AIgnitionPointBase
    {
        [SerializeField]
        ItemName itemName;
        public ItemName ItemName => itemName;
        [SerializeField]
        IGnitionPointItem ignitionPointItem;
        public IGnitionPointItem IGnitionPointItem => ignitionPointItem;
    }
}