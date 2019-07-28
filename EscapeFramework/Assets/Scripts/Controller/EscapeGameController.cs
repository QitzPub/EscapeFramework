
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
        //ItemEventView[] itemEvents;
        //SwitchEventView[] switchEvents;

        void Start()
        {
            EventProgress();
            SceneManager.sceneLoaded += SceneLoaded;
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            EventProgress();
        }

        void EventProgress()
        {
            var events = GetEventsFromCurrentScene();
            ExecuteEventsFromCurrentScene(events.Item1, events.Item2);
            SetEventsFromCurrentScene(events.Item1, events.Item2);
        }

        (ItemEventView[], SwitchEventView[]) GetEventsFromCurrentScene()
        {
            var itemEvents = UnityEngine.Object.FindObjectsOfType<ItemEventView>();
            var switchEvents = UnityEngine.Object.FindObjectsOfType<SwitchEventView>();
            return (itemEvents, switchEvents);
        }

        void ExecuteEventsFromCurrentScene(ItemEventView[] itemEvents, SwitchEventView[] switchEvents)
        {

            //シーンが読み込まれた時に実行される
            foreach (var ev in switchEvents.Where(se=>se.EventExecuteTiming == EventExecuteTiming.シーン読み込み時).Select(se=>new SwitchEventProcessingUseCase(se, repository.EscapeGameUserDataStore)))
            {
                ev.Execute();
            }
            foreach (var ev in itemEvents.Where(ie => ie.EventExecuteTiming == EventExecuteTiming.シーン読み込み時).Select(ie => new ItemEventProcessingUseCase(ie, repository.EscapeGameUserDataStore)))
            {
                ev.Execute();
            }


        }
        void SetEventsFromCurrentScene(ItemEventView[] itemEvents, SwitchEventView[] switchEvents)
        {
            //イベントアイテムViewがクリックされた時に実行される
            foreach (var ie in itemEvents.Where(ie => ie.EventExecuteTiming == EventExecuteTiming.クリックされた時))
            {
                ie.SetClickEvent(() => {
                    var ev = new ItemEventProcessingUseCase(ie, repository.EscapeGameUserDataStore);
                    ev.Execute();
                });
            }
            foreach (var se in switchEvents.Where(se => se.EventExecuteTiming == EventExecuteTiming.クリックされた時))
            {
                se.SetClickEvent(() => {
                    var ev = new SwitchEventProcessingUseCase(se, repository.EscapeGameUserDataStore);
                    ev.Execute();
                });
            }
        }
    }
}