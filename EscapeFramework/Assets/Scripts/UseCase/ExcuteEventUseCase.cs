using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Qitz.EscapeFramework
{
    public interface IExcutabel
    {
        void ExcuteSceneLoadTimingEvent();
    }
    public interface IExcuteEventUseCase: IExcutabel
    {
    }

    public class ExcuteEventUseCase: IExcuteEventUseCase
    {
        IEscapeGameUserDataStore escapeGameUserDataStore;
        AEvent[] events;
        Action<AEvent[]> eventExcuteCallBack;
        IEnumerable<AEscapeGameEvent> normalEvents;
        IEnumerable<AItemDropEvent> itemDropEvents;

        public ExcuteEventUseCase(IEscapeGameUserDataStore escapeGameUserDataStore, Action<AEvent[]> eventExcuteCallBack)
        {
            this.events = UnityEngine.Object.FindObjectsOfType<AEvent>();
            this.normalEvents = events.Select(e => e as AEscapeGameEvent).Where(e => e != null);
            this.itemDropEvents = events.Select(e => e as AItemDropEvent).Where(e => e != null);
            this.escapeGameUserDataStore = escapeGameUserDataStore;
            this.eventExcuteCallBack = eventExcuteCallBack;

            SetClickEvents();
            SetItemDropEvent();

        }

        public void ExcuteSceneLoadTimingEvent()
        {

            //シーン読み込み時開始になっているものはこのタイミングでイベントが実行される
            foreach (var aEvent in normalEvents.Where(e=>e.EventExecuteTiming == EventExecuteTiming.シーン読み込み時))
            {
                ExcuteNormalEvent(aEvent);
            }
            ExcuteUpdateEvent();
            eventExcuteCallBack.Invoke(events);
        }

        public void ExcuteUpdateEvent()
        {
            foreach (var aEvent in normalEvents.Where(e => e.EventExecuteTiming == EventExecuteTiming.Update実行))
            {
                ExcuteNormalEvent(aEvent);
            }
            eventExcuteCallBack.Invoke(events);
        }

        void SetClickEvents()
        {
            //Viewクリック時に実行されるイベントのセットを行う
            foreach (var aEvent in normalEvents.Where(e => e.EventExecuteTiming == EventExecuteTiming.クリックされた時))
            {
                SetClickEvent(aEvent);
            }
        }

        void SetItemDropEvent()
        {
            //ItemDrop時に実行されるイベントのセットを行う
            foreach (var ide in itemDropEvents)
            {
                ide.DropableView.SetDropAction((itemName) => {
                    ExcuteItemDropEvent(ide, itemName);
                    eventExcuteCallBack.Invoke(events);
                });
            }
        }

        void SetClickEvent(AEvent aEvent)
        {
            aEvent.Button.onClick.AddListener(
                () => { 
                    ExcuteNormalEvent(aEvent);
                    //UPDate時に実行されるイベントも発火する
                    ExcuteUpdateEvent();
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
            if ((aEvent as IncreaseAndDecreaseItemEvent) != null)
            {
                var itemEvent = aEvent as IncreaseAndDecreaseItemEvent;
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
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit((AEvent)eventFlagEventView);
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
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit((AEvent)displayEventView);
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
        void ExcuteItemIncreaseEvent(IIncreaseAndDecreaseItemEvent increaseItemEventView)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = JudgeEventIgnitionOverTheLimit((AEvent)increaseItemEventView);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }

            if (increaseItemEventView.ItemEventProgress == EventProgress.増やす)
            {
                escapeGameUserDataStore.AddItem(increaseItemEventView.ItemVO);
            }
            else if (increaseItemEventView.ItemEventProgress == EventProgress.減らす){
                escapeGameUserDataStore.DecreaseItem(increaseItemEventView.ItemVO);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{increaseItemEventView.ItemEventProgress}");
            }
        }

        //↓以下イベント発火制限条件を突破しているかどうかの判定取得

        bool JudgeEventIgnitionOverTheLimit(AEvent aEvent)
        {
            //制限条件が設定されていない場合
            if (!aEvent.UseItemRestrictedSetting && !aEvent.UseEventFlagRestrictedSetting)
            {
                return true;
            }
            else if(aEvent.UseItemRestrictedSetting)
            {
                return JudgeItemEventIgnitionOverTheLimit(aEvent);
            }
            else if (aEvent.UseEventFlagRestrictedSetting)
            {
                return JudgeEventFlagIgnitionOverTheLimit(aEvent);
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }

        bool JudgeItemEventIgnitionOverTheLimit(AEvent aEvent)
        {
            if(aEvent.IGnitionPointItem == IGnitionPointItem.アイテムを持っている)
            {
                return escapeGameUserDataStore.InPossessionItem(aEvent.ItemName);
            }
            else if (aEvent.IGnitionPointItem == IGnitionPointItem.アイテムを持っていない)
            {
                return !escapeGameUserDataStore.InPossessionItem(aEvent.ItemName);
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }

        }

        bool JudgeEventFlagIgnitionOverTheLimit(AEvent aEvent)
        {
            if(aEvent.EventFlag == EventFlag.ON)
            {
                return escapeGameUserDataStore.GetEventFlagValue(aEvent.EventType);
            }
            else if (aEvent.EventFlag == EventFlag.OFF)
            {
                return !escapeGameUserDataStore.GetEventFlagValue(aEvent.EventType);
            }
            else
            {
                throw new System.Exception($"想定されない形式です");
            }
        }

    }
}