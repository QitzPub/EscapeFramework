using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Qitz.EscapeFramework
{

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public abstract class AEvent : MonoBehaviour
    {
        public Button Button => this.GetComponent<Button>();
        public Image Image => this.GetComponent<Image>();

        //====================================
        [HideInInspector,HeaderAttribute("アイテム所持判定のイベント発火制限をかける")]
        public bool UseItemRestrictedSetting = false;

        [HideInInspector]
        public List<ItemIGnitionPoint> ItemIGnitions = new List<ItemIGnitionPoint>() { new ItemIGnitionPoint() };

        //====================================

        //====================================
        [HideInInspector, HeaderAttribute("フラグによるイベント発火制限をかける")]
        public bool UseEventFlagRestrictedSetting = false;

        [HideInInspector]
        public List<EventFlagIGnitionPoint> EventFlagIGnitions = new List<EventFlagIGnitionPoint>() { new EventFlagIGnitionPoint() };

        //====================================

        //====================================
        [HideInInspector, HeaderAttribute("アイテム選択状態によるイベント発火制限をかける")]
        public bool UseSelectedItemRestrictedSetting = false;

        [HideInInspector]
        public List<ItemSelectIGnitionPoint> SelectItemIGnitions = new List<ItemSelectIGnitionPoint>() { new ItemSelectIGnitionPoint() };

        //====================================

        //====================================
        [HideInInspector, HeaderAttribute("カウントイベントによる発火制限をかける")]
        public bool UseCountEventRestrictedSetting = false;

        [HideInInspector]
        public List<CountEventIGnitionPoint> CountEventIGnitions = new List<CountEventIGnitionPoint>() { new CountEventIGnitionPoint() };


        //====================================


        [HideInInspector]
        public bool UseDelay = false;
        [HideInInspector]
        public float DelayTime = 0.0f;

    }

    [Serializable]
    public class ItemIGnitionPoint
    {
        [HideInInspector]
        public ItemName ItemName;
        [HideInInspector]
        public IGnitionPointItem IGnitionPointItem;
    }

    [Serializable]
    public class EventFlagIGnitionPoint
    {
        [HideInInspector]
        public EventName EventType;
        [HideInInspector]
        public EventFlag EventFlag;
    }
    [Serializable]
    public class ItemSelectIGnitionPoint
    {
        [HideInInspector]
        public ItemName ItemName;
        [HideInInspector]
        public SelectItemState SelectItemState;
    }

    [Serializable]
    public class CountEventIGnitionPoint
    {
        [HideInInspector]
        public CountEventType CountEventName;
        [HideInInspector]
        public int CountEventValue;
        [HideInInspector]
        public CountEventJudge CountEventJudge;
    }

}