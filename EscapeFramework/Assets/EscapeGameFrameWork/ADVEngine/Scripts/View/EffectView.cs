using System.Collections;
using System.Collections.Generic;
using Qitz.ArchitectureCore.ADVGame;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;
using UniRx;
using System;

namespace Qitz.ADVGame
{
    public class EffectView : AView
    {
        [SerializeField]
        Image blackOutImage;
        [SerializeField]
        float blackOutWaitTime = 2.0f;
        [SerializeField] AWindowView _windowView;
        public float BlackOutWaitTime => blackOutWaitTime;
        Subject<Unit> blackOutEndSubject = new Subject<Unit>();
        public IObservable<Unit> BlackOutEndObservable => blackOutEndSubject;

        public async void DoEffect(ICommandWrapVO vO)
        {
            var type = vO.CommandHeadVO.CommandType;
            if(type == CommandType.BLAKOUT)
            {
                blackOutImage.gameObject.SetActive(true);
                BlackOutImageFadeIn();
                await UniTask.Delay((int)blackOutWaitTime*1000);
                BlackOutImageFadeOut();
                blackOutEndSubject.OnNext(Unit.Default);
                await UniTask.Delay(1000);
                blackOutImage.gameObject.SetActive(false);
            }else if(type == CommandType.MESSAGEOFF)
            {
                _windowView.Hide();
            }
            else if (type == CommandType.MESSAGEON)
            {
                _windowView.Show();
            }

        }

        //↓パフォーマンスやばそう・・・・
        void BlackOutImageFadeIn()
        {

            // SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
            iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "SetBlackOutImageAlpha"));
        }
        void BlackOutImageFadeOut()
        {
            // SetValue()を毎フレーム呼び出して、１秒間に１から０までの値の中間値を渡す
            iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "SetBlackOutImageAlpha"));
        }
        void SetBlackOutImageAlpha(float alpha)
        {
            // iTweenで呼ばれたら、受け取った値をImageのアルファ値にセット
            blackOutImage.color = new Color(0, 0, 0, alpha);
        }

    }
}
