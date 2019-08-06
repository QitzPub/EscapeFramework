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
        [HeaderAttribute("アイテム所持判定のイベント発火制限をかける")]
        public bool UseItemRestrictedSetting = false;

        [HideInInspector]
        public List<ItemIGnitionPoint> ItemIGnitions = new List<ItemIGnitionPoint>() { new ItemIGnitionPoint() };

        //====================================

        //====================================
        [HeaderAttribute("フラグによるイベント発火制限をかける")]
        public bool UseEventFlagRestrictedSetting = false;

        [HideInInspector]
        public List<EventFlagIGnitionPoint> EventFlagIGnitions = new List<EventFlagIGnitionPoint>() { new EventFlagIGnitionPoint() };

        //====================================

        //====================================
        [HeaderAttribute("カウントイベントによる発火制限をかける")]
        public bool UseCountEventRestrictedSetting = false;

        [HideInInspector]
        public List<CountEventIGnitionPoint> CountEventIGnitions = new List<CountEventIGnitionPoint>() { new CountEventIGnitionPoint() };


        //====================================

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
        public EventType EventType;
        [HideInInspector]
        public EventFlag EventFlag;
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