using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    //TODO 本当は機能ごとにインターフェイスを分けるのだが・・・・・・
    public interface IGameEventExecutorUseCase
    {
        bool ExcuteSceneTransitionEvent(GameEvent gameEvent);
        bool ExcuteScreenEffectEvent(GameEvent gameEvent);
        bool ExcuteWindowEvent(GameEvent gameEvent, Action<GameEvent> closeCallBack);
        bool ExcuteBGMEvent(GameEvent gameEvent);
        bool ExcuteSEEvent(GameEvent gameEvent);
        bool ExcuteEventFlagEvent(GameEvent gameEvent);
        bool ExcuteDisplayEvent(GameEvent gameEvent);
        bool ExcuteItemIncreaseEvent(GameEvent gameEvent);
        bool ExcuteCountIncreaseAndDecreaseEvent(GameEvent gameEvent);
        bool ExcuteSpriteChangeEvent(GameEvent gameEvent);
        bool ExcuteItemWindowEvent(GameEvent gameEvent);
    }

    public class GameEventExecutorUseCase: IGameEventExecutorUseCase
    {

        IEscapeGameUserDataStore escapeGameUserDataStore;
        EscapeGameAudioPlayer escapeGameAudioPlayer;
        IADVWindowView aDVWindowView;
        IScreenEffectView screenEffectView;
        ISceneTransitionUseCase sceneTransitionUseCase = new SceneTransitionUseCase();
        IJudgeIgnitionOverTheLimitUseCase judgeIgnitionOverTheLimitUseCase;
        IItemWindowView itemWindowView;

        public GameEventExecutorUseCase(IEscapeGameUserDataStore escapeGameUserDataStore, EscapeGameAudioPlayer escapeGameAudioPlayer, IADVWindowView aDVWindowView, IScreenEffectView screenEffectView, IItemWindowView itemWindowView)
        {
            this.escapeGameUserDataStore = escapeGameUserDataStore;
            this.escapeGameAudioPlayer = escapeGameAudioPlayer;
            this.aDVWindowView = aDVWindowView;
            this.screenEffectView = screenEffectView;
            this.judgeIgnitionOverTheLimitUseCase = new JudgeIgnitionOverTheLimitUseCase(escapeGameUserDataStore);
            this.itemWindowView = itemWindowView;
        }


        public bool ExcuteSceneTransitionEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            sceneTransitionUseCase.GotoScene(gameEvent.SceneName);
            return true;
        }

        //AdvWindowの表示を行う
        public bool ExcuteScreenEffectEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
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
            return true;
        }

        //AdvWindowの表示を行う
        public bool ExcuteWindowEvent(GameEvent gameEvent, Action<GameEvent> closeCallBack)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            aDVWindowView.SetText(gameEvent, closeCallBack);
            return true;
        }

        //BGMを鳴らす
        public bool ExcuteBGMEvent(GameEvent gameEvent)
        {
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            escapeGameAudioPlayer.PlayAudio(gameEvent.BGMName);
            return true;
        }

        //SE再生イベント
        public bool ExcuteSEEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            escapeGameAudioPlayer.PlaySE(gameEvent.SEName);
            return true;
        }

        //フラグ変更イベント
        public bool ExcuteEventFlagEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            escapeGameUserDataStore.SetEventFlag(gameEvent.EventFlagVO);
            return true;
        }

        //表示-非表示イベントの実行
        public bool ExcuteDisplayEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
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
            return true;
        }

        //アイテム増加イベントの実行
        public bool ExcuteItemIncreaseEvent(GameEvent gameEvent)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
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
            return true;
        }
        //カウント増減イベントの実行
        public bool ExcuteCountIncreaseAndDecreaseEvent(GameEvent gameEvent)
        {

            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
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
            return true;
        }

        public bool ExcuteSpriteChangeEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }

            gameEvent.Image.sprite = gameEvent.ChangeSprite;
            return true;
        }
        public bool ExcuteItemWindowEvent(GameEvent gameEvent)
        {
            //イベント制限事項を突破しているかどうか判定
            bool isOverTheLimit = judgeIgnitionOverTheLimitUseCase.JudgeEventIgnitionOverTheLimit((AEvent)gameEvent);
            if (!isOverTheLimit)
            {
                //イベント制限を突破していないのでイベントは実行されず
                return false;
            }
            //アイテムWindowを表示したり消したりするイベントを追加
            if(gameEvent.ItemWinodwEvent == ItemWinodwEvent.アイテム欄を表示する)
            {
                itemWindowView.Show();
            }else if (gameEvent.ItemWinodwEvent == ItemWinodwEvent.アイテム欄を非表示にする)
            {
                itemWindowView.Hide();
            }
            return true;

        }
    }
}