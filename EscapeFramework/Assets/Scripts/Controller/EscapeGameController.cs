
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
        List<ItemEvent> itemEvents;
        List<SwitchEvent> switchEvents;

        void Start()
        {
            GetEventsFromCurrentScene();
            SceneManager.sceneLoaded += SceneLoaded;
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            GetEventsFromCurrentScene();
        }
        void GetEventsFromCurrentScene()
        {
            itemEvents = UnityEngine.Object.FindObjectsOfType<ItemEvent>().ToList();
            switchEvents = UnityEngine.Object.FindObjectsOfType<SwitchEvent>().ToList();
        }
    }
}