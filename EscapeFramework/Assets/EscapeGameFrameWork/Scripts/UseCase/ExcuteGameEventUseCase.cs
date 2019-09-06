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
        void ExcuteUpdateEvent();
    }
    public interface IExcuteEventUseCase : IExcutabel
    {
    }

    public class ExcuteGameEventUseCase : IExcuteEventUseCase
    {

        GameEvent[] events;
        Action<GameEvent[]> eventExcuteCallBack;
        IEnumerable<GameEvent> normalEvents;
        IEnumerable<GameEvent> itemDropEvents;
        IEnumerable<GameEvent> chainEvents;
        IGameEventExecutorUseCase gameEventExecutor;

        public ExcuteGameEventUseCase(Action<GameEvent[]> eventExcuteCallBack, IGameEventExecutorUseCase gameEventExecutor)
        {
            this.gameEventExecutor = gameEventExecutor;
            this.eventExcuteCallBack = eventExcuteCallBack;
        }
        void SetSceneEvents()
        {
            this.events = UnityEngine.Object.FindObjectsOfType<GameEvent>();
            this.normalEvents = events.Where(e => e != null && e.EventExecuteTiming != EventExecuteTiming.アイテムがドロップされた時 && e.EventExecuteTiming != EventExecuteTiming.指定のイベントが実行完了した時);
            this.itemDropEvents = events.Where(e => e != null && e.EventExecuteTiming == EventExecuteTiming.アイテムがドロップされた時);
            this.chainEvents = events.Where(e => e != null && e.EventExecuteTiming == EventExecuteTiming.指定のイベントが実行完了した時);
            SetClickEvents();
            SetItemDropEvent();
        }

        public void ExcuteSceneLoadTimingEvent()
        {
            //シーンロードのタイミングで各種シーン中のイベントを取得し直す
            SetSceneEvents();
            //シーン読み込み時開始になっているものはこのタイミングでイベントが実行される
            foreach (var aEvent in normalEvents.Where(e => e.EventExecuteTiming == EventExecuteTiming.シーン読み込み時))
            {
                if(aEvent.DelayTime == 0)
                {
                    ExcuteEvent(aEvent);
                }
                else
                {
                    DelayTool.Tools.Delay(aEvent.DelayTime, () =>
                    {
                        ExcuteEvent(aEvent);
                    });
                }

            }
            ExcuteUpdateEvent();
            eventExcuteCallBack.Invoke(events);
        }

        public void ExcuteUpdateEvent()
        {
            foreach (var aEvent in normalEvents.Where(e => e.EventExecuteTiming == EventExecuteTiming.Update実行))
            {
                ExcuteEvent(aEvent);
            }
            eventExcuteCallBack.Invoke(events);
        }
        void SetClickEvents()
        {
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
                    DelayTool.Tools.Delay(ide.DelayTime, () => {
                        ExcuteItemDropEvent(ide, itemName);
                        eventExcuteCallBack.Invoke(events);
                    });
                });
            }
        }

        void SetClickEvent(GameEvent aEvent)
        {
            aEvent.Button.onClick.AddListener(
                () => {
                    Debug.Log($"ExcuteNormalEvent:{aEvent.name}");
                    //遅延処理する！
                    DelayTool.Tools.Delay(aEvent.DelayTime, () => {
                        ExcuteEvent(aEvent);
                        //UPDate時に実行されるイベントも発火する
                        ExcuteUpdateEvent();
                        eventExcuteCallBack.Invoke(events);
                    });
                }
            );
        }

        void ExcuteItemDropEvent(GameEvent gameEvent, ItemName dropedItem)
        {
            if (gameEvent.DropedItemName != dropedItem)
            {
                return;
            }
            ExcuteEvent(gameEvent);
        }

        void ExcuteEvent(GameEvent gameEvent)
        {
            bool eventExcuted = false;
            if (gameEvent.EventType == EventType.アイテムイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteItemIncreaseEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.表示ー非表示イベント)
            {
                eventExcuted = gameEventExecutor.ExcuteDisplayEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.フラグイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteEventFlagEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.カウントイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteCountIncreaseAndDecreaseEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.イメージ切り替えイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteSpriteChangeEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.BGMイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteBGMEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.SEイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteSEEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.メッセージWindowイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteWindowEvent(gameEvent,(_gameEvent)=>{
                    //メッセージイベントの場合はメッセージWindowが閉じられたら
                    //チェーンイベントを実行する
                    ExcuteChainEvent(_gameEvent.EventToken);
                });
            }
            else if (gameEvent.EventType == EventType.ADVイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteADVEvent(gameEvent, (_gameEvent) => {
                    //メッセージイベントの場合はメッセージWindowが閉じられたら
                    //チェーンイベントを実行する
                    ExcuteChainEvent(_gameEvent.EventToken);
                });
            }

            else if (gameEvent.EventType == EventType.スクリーンエフェクトイベント)
            {
                eventExcuted = gameEventExecutor.ExcuteScreenEffectEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.シーン遷移イベント)
            {
                eventExcuted = gameEventExecutor.ExcuteSceneTransitionEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.アイテム欄の表示ー非表示切り替え)
            {
                eventExcuted = gameEventExecutor.ExcuteItemWindowEvent(gameEvent);
            }

            //↓ここからチェイン指定があるイベントを実行する↓
            if (gameEvent.EventType != EventType.メッセージWindowイベント && gameEvent.EventType != EventType.ADVイベント && eventExcuted)
            {
                ExcuteChainEvent(gameEvent.EventToken);
            }

        }

        void ExcuteChainEvent(string exctutedEventToken)
        {
            foreach (var ce in chainEvents)
            {
                ExcuteTargetTokenEvent(ce, exctutedEventToken);
            }
        }

        void ExcuteTargetTokenEvent(GameEvent gameEvent, string exctutedEventToken)
        {
            if (gameEvent.ChainEvent.EventToken == exctutedEventToken)
            {
                //遅延処理を挟む
                DelayTool.Tools.Delay(gameEvent.DelayTime, () =>
                {
                    ExcuteEvent(gameEvent);
                });
            }
        }

    }
}