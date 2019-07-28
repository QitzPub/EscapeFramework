using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class ItemEventView : MonoBehaviour, IItemEventView,IView
    {
        [SerializeField, HeaderAttribute("イベントの実行タイミングは")]
        EventExecuteTiming eventExecuteTiming;
        [SerializeField]
        ItemName itemName;
        [SerializeField, HeaderAttribute("を")]
        ItemPossession itemPossession;
        [SerializeField, HeaderAttribute("の時")]
        DisplayEventProgress eventProgress;
        [HideInInspector]
        public ItemName AddItem;
        [HideInInspector]
        public ItemName DecreaseItem;

        public ItemName ItemName => itemName;

        public ItemPossession ItemPossession => itemPossession;

        public DisplayEventProgress EventProgress => eventProgress;

        public GameObject GameObject => this.gameObject;

        public EventExecuteTiming EventExecuteTiming => eventExecuteTiming;

        public void SetClickEvent(Action onClickAction)
        {
            Button button = this.GetComponent<Button>();
            button.onClick.AddListener(() => { onClickAction.Invoke(); });
        }
    }



    public interface IItemEventView
    {
        EventExecuteTiming EventExecuteTiming { get; }
        ItemName ItemName { get; }
        ItemPossession ItemPossession { get; }
        DisplayEventProgress EventProgress { get; }
        GameObject GameObject { get; }
        void SetClickEvent(Action onClickAction);
    }



}