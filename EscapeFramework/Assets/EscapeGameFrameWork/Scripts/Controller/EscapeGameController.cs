﻿
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameController
    {
        void AddEventExecuteCallBack(Action<AEvent[]> addEventExecuteCallBack);
        void AddUserItemListChangeCallBack(Action<List<IItemSpriteVO>> addUserItemListChangeCallBack);
        IEscapeGameDefinsDataStore GetEscapeGameDefins();
    }

    public class EscapeGameController : AController<EscapeGameRepository>, IEscapeGameController
    {
        [SerializeField]
        EscapeGameRepository repository;
        protected override EscapeGameRepository Repository { get { return repository; } }
        Action<AEvent[]> eventExecuteCallBack;
        Action<List<IItemSpriteVO>> userItemListChangeCallBack;
        IExcuteEventUseCase excuteEventUseCase;
        [SerializeField]
        EscapeGameAudioPlayer escapeGameAudioPlayer;
        [SerializeField]
        ADVWindowView aDVWindowView;
        [SerializeField]
        ScreenEffectView screenEffectView;
        [SerializeField]
        ItemWindowView itemWindowView;
        ItemName selectedItem;
        ItemSelectUseCase itemSelectUseCase;

        public void AddEventExecuteCallBack(Action<AEvent[]> addEventExecuteCallBack)
        {
            eventExecuteCallBack += addEventExecuteCallBack;
        }

        public void AddUserItemListChangeCallBack(Action<List<IItemSpriteVO>> addUserItemListChangeCallBack)
        {
            userItemListChangeCallBack += addUserItemListChangeCallBack;
        }

        void Awake()
        {
            aDVWindowView.Close();
            repository.Initialize();
            itemSelectUseCase = new ItemSelectUseCase(itemWindowView);
            var gameEventExecutorUseCase = new GameEventExecutorUseCase(Repository.EscapeGameUserDataStore, escapeGameAudioPlayer, aDVWindowView, screenEffectView, itemSelectUseCase);

            excuteEventUseCase = new ExcuteGameEventUseCase(
                (events) => {
                    //イベント実行後のコールバック
                    //ユーザーデータのアイテム数をItemViewに反映させる処理など
                    eventExecuteCallBack?.Invoke(events);
                    userItemListChangeCallBack?.Invoke(repository.UserPossessionItemSpriteList);
                }
                    , gameEventExecutorUseCase
                    , itemSelectUseCase
                    );
        }

        void Start()
        {
            escapeGameAudioPlayer.Initialize(Repository.EscapeGameAudioDataStore);
            ExecuteEvent(excuteEventUseCase);
            StartCoroutine(ExcuteUpdateEvent(excuteEventUseCase));
            SceneManager.sceneLoaded += SceneLoaded;
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            screenEffectView.TerminateScreenEffect();
            ExecuteEvent(excuteEventUseCase);
        }

        void ExecuteEvent(IExcuteEventUseCase excuteEventUseCase)
        {
            excuteEventUseCase.ExcuteSceneLoadTimingEvent();
        }

        //UpdateEventを実行するタイムスパン
        const float UPDATE_EVENT_EXCUTE_SPAN = 1.0f;
        IEnumerator ExcuteUpdateEvent(IExcuteEventUseCase excuteEventUseCase)
        {
            while (true)
            {
                yield return new WaitForSeconds(UPDATE_EVENT_EXCUTE_SPAN);
                excuteEventUseCase.ExcuteUpdateEvent();
            }
            yield return null;
        }

        public IEscapeGameDefinsDataStore GetEscapeGameDefins()
        {
            return repository.EscapeGameDefinsDataStore;
        }

        [ContextMenu("ユーザーデータを削除")]
        void DebugDeleteUserData()
        {
            PlayerPrefs.DeleteAll();
        }


        public void ClearUserData()
        {
            repository.EscapeGameUserDataStore.ClearUserData();
        }

        public void DumpUserdata()
        {
            var data = Repository.EscapeGameUserDataStore;
            foreach (var item in data.Items)
            {
                Debug.Log(item.ItemName);
            }
            foreach (var c in data.CountEvents)
            {
                Debug.Log(c.CountEventType+":"+c.Count);
            }
            foreach (var ev in data.EventFlags)
            {
                Debug.Log(ev.EventType+":"+ ev.IsOn);
            }
        }

        //TODO ↓ItemProgressUseCase
        public void SelectItem(ItemName item)
        {
            itemSelectUseCase.SelectItem(item);
        }

     }
}