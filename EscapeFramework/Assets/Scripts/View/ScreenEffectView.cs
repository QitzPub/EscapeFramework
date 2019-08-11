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
        CanvasAlphaTween canvasAlphaTween;
        [SerializeField]
        CanvasGroup canvasGroup;
        [SerializeField]
        Image image;

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
            canvasAlphaTween.SetAlphaTween(0,1,1);
        }
        public void UnBlackOut()
        {
            canvasAlphaTween.SetAlphaTween(1, 0, 1);
        }

        public void TerminateScreenEffect()
        {
            UnBlockRaycasts();
            canvasGroup.alpha = 0;
        }
    }
}