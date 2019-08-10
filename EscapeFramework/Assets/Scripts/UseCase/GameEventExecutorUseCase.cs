using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    //TODO 本当は機能ごとにインターフェイスを分けるのだが・・・・・・
    public interface IGameEventExecutorUseCase
    {
        void ExcuteSceneTransitionEvent(GameEvent gameEvent);
        void ExcuteScreenEffectEvent(GameEvent gameEvent);
        void ExcuteWindowEvent(GameEvent gameEvent, Action<GameEvent> closeCallBack);
        void ExcuteBGMEvent(GameEvent gameEvent);
        void ExcuteSEEvent(GameEvent gameEvent);
        void ExcuteEventFlagEvent(GameEvent gameEvent);
        void ExcuteDisplayEvent(GameEvent gameEvent);
        void ExcuteItemIncreaseEvent(GameEvent gameEvent);
        void ExcuteCountIncreaseAndDecreaseEvent(GameEvent gameEvent);
        void ExcuteSpriteChangeEvent(GameEvent gameEvent);
    }

    public class GameEventExecutorUseCase: IGameEventExecutorUseCase
    {

        IEscapeGameUserDataStore escapeGameUserDataStore;
        EscapeGameAudioPlayer escapeGameAudioPlayer;
        IADVWindowView aDVWindowView;
        IScreenEffectView screenEffectView;
        ISceneTransitionUseCase sceneTransitionUseCase = new SceneTransitionUseCase();
        IJudgeIgnitionOverTheLimitUseCase judgeIgnitionOverTheLimitUseCase;

        public GameEventExecutorUseCase(IEscapeGameUserDataStore escapeGameUserDataStore, EscapeGameAudioPlayer escapeGameAudioPlayer, IADVWindowView aDVWindowView, IScreenEffectView screenEffectView)
        {
            this.escapeGameUserDataStore = escapeGameUserDataStore;
            this.escapeGameAudioPlayer = escapeGameAudioPlayer;
            this.aDVWindowView = aDVWindowView;
            this.screenEffectView = screenEffectView;
            this.judgeIgnitionOverTheLimitUseCase = new JudgeIgnitionOverTheLimitUseCase(escapeGameUserDataStore);
        }


        public void ExcuteSceneTransitionEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            sceneTransitionUseCase.GotoScene(gameEvent.SceneName);
            screenEffectView.TerminateScreenEffect();
        }

        //AdvWindowの表示を行う
        public void ExcuteScreenEffectEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            switch (gameEvent.ScreenEffect)
            {
                case ScreenEffectName.画面操作不能にする:
                    screenEffectView.BlockRaycasts();
                    break;
                case ScreenEffectName.画面操作不能解除:
                    screenEffectView.UnBlockRaycasts();
                    break;
                case ScreenEffectName.画面暗転:
                    screenEffectView.BlackOut();
                    break;
                case ScreenEffectName.画面暗転解除:
                    screenEffectView.UnBlackOut();
                    break;
                default:
                    throw new Exception("想定されない型です。");
                    break;
            }
        }

        //AdvWindowの表示を行う
        public void ExcuteWindowEvent(GameEvent gameEvent, Action<GameEvent> closeCallBack)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            aDVWindowView.SetText(gameEvent, closeCallBack);
        }

        //BGMを鳴らす
        public void ExcuteBGMEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            escapeGameAudioPlayer.PlayAudio(gameEvent.BGMName);
        }

        //SE再生イベント
        public void ExcuteSEEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            escapeGameAudioPlayer.PlaySE(gameEvent.SEName);
        }

        //フラグ変更イベント
        public void ExcuteEventFlagEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            escapeGameUserDataStore.SetEventFlag(gameEvent.EventFlagVO);
        }

        //表示-非表示イベントの実行
        public void ExcuteDisplayEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }
            if (gameEvent.DisplayEventProgress == DisplayEventProgress.表示する)
            {
                gameEvent.gameObject.SetActive(true);
            }
            else if (gameEvent.DisplayEventProgress == DisplayEventProgress.非表示にする)
            {
                gameEvent.gameObject.SetActive(false);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{gameEvent.DisplayEventProgress}");
            }
        }

        //アイテム増加イベントの実行
        public void ExcuteItemIncreaseEvent(GameEvent gameEvent)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }

            if (gameEvent.ItemEventProgress == EventProgress.増やす)
            {
                escapeGameUserDataStore.AddItem(gameEvent.ItemVO);
            }
            else if (gameEvent.ItemEventProgress == EventProgress.減らす)
            {
                escapeGameUserDataStore.DecreaseItem(gameEvent.ItemVO);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{gameEvent.ItemEventProgress}");
            }
        }
        //カウント増減イベントの実行
        public void ExcuteCountIncreaseAndDecreaseEvent(GameEvent gameEvent)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }


            if (gameEvent.CountEventProgress == CountEventProgress.増やす)
            {
                escapeGameUserDataStore.IncrementEventCount(gameEvent.CountEventName);
            }
            else if (gameEvent.CountEventProgress == CountEventProgress.減らす)
            {
                escapeGameUserDataStore.DecrementEventCount(gameEvent.CountEventName);
            }
            else if (gameEvent.CountEventProgress == CountEventProgress.初期値0にする)
            {
                escapeGameUserDataStore.SetDefaultEventCount(gameEvent.CountEventName);
            }
            else
            {
                throw new System.Exception($"想定されない形式です:{gameEvent.CountEventName}");
            }
        }

        public void ExcuteSpriteChangeEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return;
            }

            gameEvent.Image.sprite = gameEvent.ChangeSprite;
        }
    }
}