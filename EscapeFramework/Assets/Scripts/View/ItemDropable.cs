using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Qitz.EscapeFramework
{
    public class ItemDropable : MonoBehaviour
    {
        Action<ItemName> dropAction;
        public Action<ItemName> DropAction => dropAction;

        public void SetDropAction(Action<ItemName> dropAction)
        {
            this.dropAction += dropAction;
        }

    }
}
