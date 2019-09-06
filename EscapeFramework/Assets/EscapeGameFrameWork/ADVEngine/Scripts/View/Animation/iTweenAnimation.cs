using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;

namespace Qitz.ADVGame
{
    public class iTweenAnimation : MonoBehaviour
    {
        enum TweenType
        {
            NONE,
            SCALE,
            LOCAL_POSTION_X,
            LOCAL_POSTION_Y,
            ALPHA,
        }
        [SerializeField]
        TweenType tweenType;
        [SerializeField]
        float from = 0;
        [SerializeField]
        float to = 0;
        [SerializeField]
        float time=0;
        CanvasGroup canvasGroup;
        [SerializeField]
        iTween.EaseType easeType;
        [SerializeField]
        int delayMiliSec = 0;
        [SerializeField]
        bool autoPlay;
        public bool IsAnimating { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (autoPlay)
            {
                DoTween();
            }
        }

        //private void OnDisable()
        //{

        //}
        public void Reset()
        {
            switch (tweenType)
            {
                case TweenType.SCALE:
                    {
                        this.transform.localScale = new Vector3(from, from);
                    }
                    break;
                case TweenType.LOCAL_POSTION_X:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(from, localPos.y);
                    }
                    break;
                case TweenType.LOCAL_POSTION_Y:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(localPos.x, from);
                    }
                    break;
                case TweenType.ALPHA:
                    {
                        canvasGroup.alpha = 1;
                    }
                    break;
                default:
                    break;
            }
        }

        [ContextMenu("TweenAnimation実行")]
        public async void DoTween()
        {
            IsAnimating = true;
            if (tweenType == TweenType.ALPHA)
            {
                canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
                if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            }
            //初期値を先にセット
            SetInitValue();
            //Delay
            await UniTask.Delay(delayMiliSec);

            iTween.ValueTo(this.gameObject, iTween.Hash("from", from, "to", to, "time", time, "onupdate", "TweenAction", "oncomplete", "CompleteAnimation","oncompletetarget", this.gameObject, "EaseType", easeType));
        }

        void SetInitValue()
        {
            switch (tweenType)
            {
                case TweenType.SCALE:
                    {
                        this.transform.localScale = new Vector3(from, from);
                    }
                    break;
                case TweenType.LOCAL_POSTION_X:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(from, localPos.y);
                    }
                    break;
                case TweenType.LOCAL_POSTION_Y:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(localPos.x, from);
                    }
                    break;
                case TweenType.ALPHA:
                    {
                        canvasGroup.alpha = from;
                    }
                    break;
                default:
                    break;
            }
        }

        void TweenAction(float value)
        {
            switch (tweenType)
            {
                case TweenType.SCALE:
                    {
                        this.transform.localScale = new Vector3(value, value);
                    }
                    break;
                case TweenType.LOCAL_POSTION_X:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(value, localPos.y);
                    }
                    break;
                case TweenType.LOCAL_POSTION_Y:
                    {
                        var localPos = this.transform.localPosition;
                        this.transform.localPosition = new Vector3(localPos.x, value);
                    }
                    break;
                case TweenType.ALPHA:
                    {
                        if(canvasGroup != null)
                        {
                            canvasGroup.alpha = value;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void CompleteAnimation()
        {
            IsAnimating = false;
        }

    }
}