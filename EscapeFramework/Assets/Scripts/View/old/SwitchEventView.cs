using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework {

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class SwitchEventView : MonoBehaviour, ISwitchEvent, IView
    {
        [SerializeField, HeaderAttribute("イベントの実行タイミングは")]
        EventExecuteTiming eventExecuteTiming;
        [SerializeField]
        EventType eventType;
        [SerializeField, HeaderAttribute("が")]
        EventFlag eventFlag;
        [SerializeField, HeaderAttribute("の時")]
        DisplayEventProgress eventProgress;

        public ItemName AddItem;
        public ItemName DecreaseItem;

        public GameObject GameObject => this.gameObject;

        public EventType EventType => eventType;

        public EventFlag EventFlag => eventFlag;

        public DisplayEventProgress EventProgress => eventProgress;

        public IEventFlagVO EventFlagVO => new EventFlagVO(eventType, eventFlag == EventFlag.ON);

        public EventExecuteTiming EventExecuteTiming => eventExecuteTiming;

        public void SetClickEvent(Action onClickAction)
        {
            Button button = this.GetComponent<Button>();
            button.onClick.AddListener(()=> { onClickAction.Invoke(); });
        }
    }



    public interface ISwitchEvent
    {
        EventExecuteTiming EventExecuteTiming { get; }
        EventType EventType { get; }
        EventFlag EventFlag { get; }
        DisplayEventProgress EventProgress { get; }
        GameObject GameObject { get; }
        IEventFlagVO EventFlagVO { get; }
        void SetClickEvent(Action onClickAction);
    }


}
