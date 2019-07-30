using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public abstract class AEvent : MonoBehaviour
    {
        public Button Button => this.GetComponent<Button>();

        //====================================
        [HeaderAttribute("アイテム所持判定のイベント発火制限をかける")]
        public bool UseItemRestrictedSetting = false;

        [HideInInspector]
        public ItemName ItemName;
        [HideInInspector]
        public IGnitionPointItem IGnitionPointItem;

        //====================================

        //====================================
        [HeaderAttribute("フラグによるイベント発火制限をかける")]
        public bool UseEventFlagRestrictedSetting = false;

        [HideInInspector]
        public EventType EventType;
        [HideInInspector]
        public EventFlag EventFlag;

        //====================================

    }

}