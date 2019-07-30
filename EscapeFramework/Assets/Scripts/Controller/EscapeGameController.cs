
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
        ExcuteEventUseCase excuteEventUseCase;

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
            repository.Initialize();
            excuteEventUseCase = new ExcuteEventUseCase(repository.EscapeGameUserDataStore,
                (events) => {
                                //イベント実行後のコールバック
                                //ユーザーデータのアイテム数をItemViewに反映させる処理など
                                eventExecuteCallBack?.Invoke(events);
                                userItemListChangeCallBack?.Invoke(repository.UserPossessionItemSpriteList);
                    });
        }

        void Start()
        {
            ExecuteEvent(excuteEventUseCase);
            StartCoroutine(ExcuteUpdateEvent(excuteEventUseCase));
            SceneManager.sceneLoaded += SceneLoaded;
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            ExecuteEvent(excuteEventUseCase);
        }

        void ExecuteEvent(ExcuteEventUseCase excuteEventUseCase)
        {
            excuteEventUseCase.ExcuteSceneLoadTimingEvent();
        }

        //UpdateEventを実行するタイムスパン
        const float UPDATE_EVENT_EXCUTE_SPAN = 1.0f;
        IEnumerator ExcuteUpdateEvent(ExcuteEventUseCase excuteEventUseCase)
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

    }
}