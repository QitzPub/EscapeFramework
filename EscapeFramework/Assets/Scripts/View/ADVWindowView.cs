using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Qitz.EscapeFramework
{
    public interface IADVWindowView
    {
        void Show();
        void Close();
        void SetText(GameEvent gameEvent, Action<GameEvent> closeCallBack);
    }

    public class ADVWindowView : MonoBehaviour,IADVWindowView
    {
        [SerializeField]
        TextAnimation textAnimation;
        List<string> texts;
        int currentCount = 0;
        GameEvent gameEvent;
        Action<GameEvent> closeCallBack;

        public void Close()
        {
            textAnimation.SetText("");
            closeCallBack?.Invoke(gameEvent);
            this.gameObject.SetActive(false);
        }

        public void SetText(GameEvent gameEvent, Action<GameEvent> closeCallBack)
        {
            Show();
            this.gameEvent = gameEvent;
            this.texts = gameEvent.Texts;
            this.currentCount = 0;
            this.closeCallBack = closeCallBack;
            NextText();

        }

        public void NextText()
        {

            if(texts.Count <= currentCount)
            {
                Close();
                return;
            }

            string text = texts[currentCount];
            textAnimation.SetNextLine(text);
            currentCount++;
        }


        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

    }
}