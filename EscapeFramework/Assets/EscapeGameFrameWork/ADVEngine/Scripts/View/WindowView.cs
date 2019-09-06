using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx;
using UniRx.Triggers;
using System.Threading;

namespace Qitz.ADVGame
{
    public class WindowView : AWindowView
    {
        [SerializeField]
        Text name;
        [SerializeField]
        Text body;
        [SerializeField]
        TextAnimation bodyTextAnimation;
        [SerializeField]
        GameObject nameDisplay;
        [SerializeField]
        GameObject bodyDisplay;
        [SerializeField]
        GameObject pageNextAnimation;
        public override bool IsTyping { 
            get {
                if (!bodyDisplay.activeSelf) return false;
                return !bodyTextAnimation.IsCompleteDisplayText;
            }
        }

        public override void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public async override void SetWindowVO(IWindowVO vo)
        {
            //body.text = vo.WindowText;
            pageNextAnimation.SetActive(false);
            bodyTextAnimation.SetNextLine(vo.WindowText.Replace("@\\n", Environment.NewLine));
            name.text = vo.WindowCharacterName;
            if(vo.WindowCharacterName == "")
            {
                nameDisplay.SetActive(false);
            }
            else
            {
                nameDisplay.SetActive(true);
            }
            if(vo.WindowText == "")
            {
                bodyDisplay.SetActive(false);
            }
            else
            {
                bodyDisplay.SetActive(true);
            }

            await this.UpdateAsObservable()
                  .Where(_ => bodyTextAnimation.IsCompleteDisplayText || !bodyDisplay.activeSelf).Take(1).ToTask().AddTo(this.gameObject);

            pageNextAnimation.SetActive(true);

        }

        public override void Show()
        {
            this.gameObject.SetActive(true);
        }

        public override void ShowAllText()
        {
            bodyTextAnimation.ShowAllText();
        }
    }
}
