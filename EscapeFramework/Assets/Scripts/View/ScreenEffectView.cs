using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public interface IScreenEffectView
    {
        void BlockRaycasts();
        void UnBlockRaycasts();
        void BlackOut();
        void UnBlackOut();
        void TerminateScreenEffect();
    }

    public class ScreenEffectView : MonoBehaviour, IScreenEffectView
    {
        [SerializeField]
        CanvasGroup canvasGroup;
        [SerializeField]
        Image image;

        bool fadeOutRq = false;
        [SerializeField]
        float effectDuration = 1.0f;
        float currentEffectTime = 1.0f;
        float effectValue => currentEffectTime / effectDuration;

        // Start is called before the first frame update
        void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        void Update()
        {
            currentEffectTime += Time.deltaTime;
            if (fadeOutRq && canvasGroup.alpha <= 1)
            {
                canvasGroup.alpha = effectValue;
            }
            else if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha = 1 - effectValue;
            }
        }
        public void BlockRaycasts()
        {
            canvasGroup.blocksRaycasts = true;
        }
        public void UnBlockRaycasts()
        {
            canvasGroup.blocksRaycasts = false;
        }
        public void BlackOut()
        {
            canvasGroup.blocksRaycasts = true;
            fadeOutRq = true;
            currentEffectTime = 0;
        }
        public void UnBlackOut()
        {
            canvasGroup.blocksRaycasts = false;
            fadeOutRq = false;
            currentEffectTime = 0;
        }

        public void TerminateScreenEffect()
        {
            fadeOutRq = false;
            //currentEffectTime = 1.0f;
            canvasGroup.alpha = 0;
        }
    }
}