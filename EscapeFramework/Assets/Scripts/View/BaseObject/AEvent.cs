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
        public Image Image => this.GetComponent<Image>();

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

        //====================================
        [HeaderAttribute("カウントイベントによるイベント発火制限をかける")]
        public bool UseCountEventRestrictedSetting = false;

        [HideInInspector]
        public CountEventType CountEventName;
        [HideInInspector]
        public int CountEventValue;
        [HideInInspector]
        public CountEventJudge CountEventJudge;

        //====================================

    }

}