using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public class IncreaseItemEventView : AEventViewBase
    {
        [SerializeField, HeaderAttribute("以下アイテムを")]
        ItemName itemName;
        [SerializeField]
        ItemEventProgress itemEventProgress;

        public ItemName ItemName => itemName;
        public ItemEventProgress ItemEventProgress => itemEventProgress;

    }
}
