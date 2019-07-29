using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Qitz.EscapeFramework
{
    public interface IExcutabel
    {
        void Excute();
    }
    public interface IExcuteEventUseCase: IExcutabel
    {
    }

    public class ExcuteEventUseCase: IExcuteEventUseCase
    {
        IEscapeGameUserDataStore escapeGameUserDataStore;
        AEvent[] events;
        Action<AEvent[]> eventExcuteCallBack;

        public ExcuteEventUseCase(IEscapeGameUserDataStore escapeGameUserDataStore, Action<AEvent[]> eventExcuteCallBack)
        {
            this.events = UnityEngine.Object.FindObjectsOfType<AEvent>();
            this.escapeGameUserDataStore = escapeGameUserDataStore;
            this.eventExcuteCallBack = eventExcuteCallBack;
        }

        public void Excute()
        {
            var normalEvents = events.Select(e => e as AEscapeGameEvent).Where(e => e != null);
            var itemDropEvents = events.Select(e => e as AItemDropEvent).Where(e => e != null);
            //シーン読み込み時開始になっているものはこのタイミングでイベントが実行される
            foreach (var aEvent in normalEvents.Where(e=>e.EventExecuteTiming == EventExecuteTiming.シーン読み込み時))
            {
                ExcuteNormalEvent(aEvent);
            }
            //Viewクリック時に実行されるイベントのセットを行う
            foreach (var aEvent in normalEvents.Where(e => e.EventExecuteTiming == EventExecuteTiming.クリックされた時))
            {
                SetClickEvent(aEvent);
            }
            //ItemDrop時に実行されるイベントのセットを行う
            foreach (var ide in itemDropEvents)
            {
                ide.DropableView.SetDropAction((itemName)=> {
                    ExcuteItemDropEvent(ide, itemName);
                    eventExcuteCallBack.Invoke(events);
                });
            }
            eventExcuteCallBack.Invoke(events);

        }

        void SetClickEvent(AEvent aEvent)
        {
            aEvent.Button.onClick.AddListener(
                () => { 
                    ExcuteNormalEvent(aEvent);
                    eventExcuteCallBack.Invoke(events);
                }
            );
        }

        void ExcuteItemDropEvent(AItemDropEvent itemDropEvent,ItemName dropedItem)
        {
            //アイテムドロップ後に実行される処理！　実装する！！
            if(itemDropEvent.DropedItemName != dropedItem)
            {
                return;
            }

            if ((itemDropEvent as ItemDropIncreaseItemEvent) != null)
            {
                var itemEvent = itemDropEvent as ItemDropIncreaseItemEvent;
                ExcuteItemIncreaseEvent(itemEvent);
            }
            else if ((itemDropEvent as ItemDropDisplayEvent) != null)
            {
                var displayEvent = itemDropEvent as ItemDropDisplayEvent;
                ExcuteDisplayEvent(displayEvent);
            }
            else if ((itemDropEvent as ItemDropEventFlagEvent) != null)
            {
                var eventFlagEvent = itemDropEvent as ItemDropEventFlagEvent;
                ExcuteEventFlagEvent(eventFlagEvent);
            }

        }

        void ExcuteNormalEvent(AEvent aEvent)
        {
            if ((aEvent as IncreaseItemEvent) != null)
            {
                var itemEvent = aEvent as IncreaseItemEvent;
                ExcuteItemIncreaseEvent(itemEvent);
            }
            else if ((aEvent as DisplayEvent) != null)
            {
                var displayEvent = aEvent as DisplayEvent;
                ExcuteDisplayEvent(displayEvent);
            }
            else if ((aEvent as EventFlagEvent) != null)
            {
                var eventFlagEvent = aEvent as EventFlagEvent;
                ExcuteEventFlagEvent(eventFlagEvent);
            }
        }

        void ExcuteEventFlagEvent(IEventFlagEvent eventFlagEventView)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit(eventFlagEventView.AIgnitionPointBase);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            escapeGameUserDataStore.SetEventFlag(eventFlagEventView.EventFlagVO);
        }

        //表示-非表示イベントの実行
        void ExcuteDisplayEvent(IDisplayEvent displayEventView)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit(displayEventView.AIgnitionPointBase);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            if(displayEventView.DisplayEventProgress == DisplayEventProgress.表示する)
            {
                displayEventView.gameObject.SetActive(true);

            }
            else if (displayEventView.DisplayEventProgress == DisplayEventProgress.非表示にする)
            {
                displayEventView.gameObject.SetActive(false);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{displayEventView.DisplayEventProgress}");
            }
        }

        //アイテム増加イベントの実行
        void ExcuteItemIncreaseEvent(IIncreaseItemEvent increaseItemEventView)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit(increaseItemEventView.AIgnitionPointBase);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }

            if (increaseItemEventView.ItemEventProgress == ItemEventProgress.アイテムを増やす)
            {
                escapeGameUserDataStore.AddItem(increaseItemEventView.ItemVO);
            }
            else if (increaseItemEventView.ItemEventProgress == ItemEventProgress.アイテムを減らす)
            {
                escapeGameUserDataStore.DecreaseItem(increaseItemEventView.ItemVO);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{increaseItemEventView.ItemEventProgress}");
            }
        }

        //↓以下イベント発火制限条件を突破しているかどうかの判定取得

        bool JudgeEventIgnitionOverTheLimit(AIgnitionPointBase aIgnitionPoint)
        {
            //制限条件が設定されていない場合
            if (aIgnitionPoint == null)
            {
                return true;
            }
            else if((aIgnitionPoint as ItemIGnitionPointSetting) != null)
            {
                var itemIgnition = aIgnitionPoint as ItemIGnitionPointSetting;
                return JudgeItemEventIgnitionOverTheLimit(itemIgnition);
            }
            else if ((aIgnitionPoint as EventFlagIGnitionPointSetting) != null)
            {
                var eventFlagIgnition = aIgnitionPoint as EventFlagIGnitionPointSetting;
                return JudgeEventFlagIgnitionOverTheLimit(eventFlagIgnition);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{aIgnitionPoint}");
            }
        }

        bool JudgeItemEventIgnitionOverTheLimit(ItemIGnitionPointSetting aItemIgnitionPoint)
        {
            if(aItemIgnitionPoint.IGnitionPointItem == IGnitionPointItem.アイテムを持っている)
            {
                return escapeGameUserDataStore.InPossessionItem(aItemIgnitionPoint.ItemName);
            }
            else if (aItemIgnitionPoint.IGnitionPointItem == IGnitionPointItem.アイテムを持っていない)
            {
                return !escapeGameUserDataStore.InPossessionItem(aItemIgnitionPoint.ItemName);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{aItemIgnitionPoint.IGnitionPointItem}");
            }

        }

        bool JudgeEventFlagIgnitionOverTheLimit(EventFlagIGnitionPointSetting aEventFlagIgnitionPoint)
        {
            if(aEventFlagIgnitionPoint.EventFlag == EventFlag.ON)
            {
                return escapeGameUserDataStore.GetEventFlagValue(aEventFlagIgnitionPoint.EventType);
            }
            else if (aEventFlagIgnitionPoint.EventFlag == EventFlag.OFF)
            {
                return !escapeGameUserDataStore.GetEventFlagValue(aEventFlagIgnitionPoint.EventType);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{aEventFlagIgnitionPoint.EventFlag}");
            }
        }

    }
}