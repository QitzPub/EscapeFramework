using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        AEventViewBase[] events;

        public ExcuteEventUseCase(IEscapeGameUserDataStore escapeGameUserDataStore)
        {
            this.events = UnityEngine.Object.FindObjectsOfType<AEventViewBase>();
            this.escapeGameUserDataStore = escapeGameUserDataStore;
        }

        public void Excute()
        {
            //シーン読み込み時開始になっているものはこのタイミングでイベントが実行される
            foreach (var aEvent in events.Where(e=>e.EventExecuteTiming == EventExecuteTiming.シーン読み込み時))
            {
                ExcuteEvent(aEvent);
            }
            //Viewクリック時に実行されるイベントのセットを行う
            foreach (var aEvent in events.Where(e => e.EventExecuteTiming == EventExecuteTiming.クリックされた時))
            {
                SetClickEvent(aEvent);
            }
        }

        void SetClickEvent(AEventViewBase aEvent)
        {
            aEvent.Button.onClick.AddListener(
                () => { 
                    ExcuteEvent(aEvent); 
                }
            );
        }

        void ExcuteEvent(AEventViewBase aEvent)
        {
            if ((aEvent as IncreaseItemEventView) != null)
            {
                var itemEvent = aEvent as IncreaseItemEventView;
                ExcuteItemIncreaseEvent(itemEvent);
            }
            else if ((aEvent as DisplayEventView) != null)
            {
                var displayEvent = aEvent as DisplayEventView;
                ExcuteDisplayEvent(displayEvent);
            }
            else if ((aEvent as EventFlagEventView) != null)
            {
                var eventFlagEvent = aEvent as EventFlagEventView;
                ExcuteEventFlagEvent(eventFlagEvent);
            }
        }

        void ExcuteEventFlagEvent(EventFlagEventView eventFlagEventView)
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
        void ExcuteDisplayEvent(DisplayEventView displayEventView)
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
        void ExcuteItemIncreaseEvent(IncreaseItemEventView increaseItemEventView)
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