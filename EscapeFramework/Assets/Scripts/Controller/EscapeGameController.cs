
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

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
        }

        void Start()
        {
            ExecuteEvent();
            SceneManager.sceneLoaded += SceneLoaded;
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            ExecuteEvent();
        }

        void ExecuteEvent()
        {
            var excuteEventUseCase = new ExcuteEventUseCase(repository.EscapeGameUserDataStore,
                (events)=> {
                    //イベント実行後のコールバック
                    //ユーザーデータのアイテム数をItemViewに反映させる処理など
                    eventExecuteCallBack?.Invoke(events);
                    userItemListChangeCallBack?.Invoke(repository.UserPossessionItemSpriteList);
            });
            excuteEventUseCase.Excute();
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