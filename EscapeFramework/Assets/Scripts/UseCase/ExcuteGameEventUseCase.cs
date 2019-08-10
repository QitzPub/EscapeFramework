﻿using System.Collections;
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
                DelayTool.Tools.Delay(aEvent.DelayTime, () =>
                {
                    ExcuteEvent(aEvent);
                });
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
            if (gameEvent.EventType == EventType.アイテムイベント)
            {
                gameEventExecutor.ExcuteItemIncreaseEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.表示ー非表示イベント)
            {
                gameEventExecutor.ExcuteDisplayEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.フラグイベント)
            {
                gameEventExecutor.ExcuteEventFlagEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.カウントイベント)
            {
                gameEventExecutor.ExcuteCountIncreaseAndDecreaseEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.イメージ切り替えイベント)
            {
                gameEventExecutor.ExcuteSpriteChangeEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.BGMイベント)
            {
                gameEventExecutor.ExcuteBGMEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.SEイベント)
            {
                gameEventExecutor.ExcuteSEEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.メッセージWindowイベント)
            {
                gameEventExecutor.ExcuteWindowEvent(gameEvent,(_gameEvent)=>{
                    //メッセージイベントの場合はメッセージWindowが閉じられたら
                    //チェーンイベントを実行する
                    ExcuteChainEvent(_gameEvent.EventToken);
                });
            }
            else if (gameEvent.EventType == EventType.スクリーンエフェクトイベント)
            {
                gameEventExecutor.ExcuteScreenEffectEvent(gameEvent);
            }
            else if (gameEvent.EventType == EventType.シーン遷移イベント)
            {
                gameEventExecutor.ExcuteSceneTransitionEvent(gameEvent);
            }

            //↓ここからチェイン指定があるイベントを実行する↓
            if (gameEvent.EventType != EventType.メッセージWindowイベント)
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