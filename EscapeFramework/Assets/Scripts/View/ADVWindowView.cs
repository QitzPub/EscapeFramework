using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qitz.EscapeFramework
{
    public interface IADVWindowView
    {
        void Show();
        void Hide();
        void SetText(List<string> texts);
    }

    public class ADVWindowView : MonoBehaviour,IADVWindowView
    {
        [SerializeField]
        TextAnimation textAnimation;
        List<string> texts;
        int currentCount = 0;

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void SetText(List<string> texts)
        {
            Show();
            this.texts = texts;
            this.currentCount = 0;
            NextText();

        }

        public void NextText()
        {

            if(texts.Count <= currentCount)
            {
                Hide();
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