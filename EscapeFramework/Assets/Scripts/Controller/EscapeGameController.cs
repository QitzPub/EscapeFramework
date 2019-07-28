
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameController
    {
    }

    public class EscapeGameController : AController<EscapeGameRepository>, IEscapeGameController
    {
        [SerializeField]
        EscapeGameRepository repository;
        protected override EscapeGameRepository Repository { get { return repository; } }

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
            var excuteEventUseCase = new ExcuteEventUseCase(repository.EscapeGameUserDataStore);
            excuteEventUseCase.Excute();
        }

    }
}