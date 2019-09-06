using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Qitz.ArchitectureCore.ADVGame;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.ADVGame
{
    public class CharacterView : AView
    {
        bool isShowed = false;

        [SerializeField]
        Image characterBodyImage;
        public Image CharacterBodyImage => characterBodyImage;
        [SerializeField]
        Image characterFaceImage;
        public Image CharacterFaceImage => characterFaceImage;
        [SerializeField]
        CanvasGroup canvasGroup;
        ICaracterVO caracterVO;
        public bool AppendCharacter => caracterVO == null ? false: caracterVO.AppendCharacter;
        public bool DisAppendCharacter => caracterVO == null ? false : caracterVO.DisAppendCharacter;
        //単位はmiliSec
        int effectTime
        {
            get
            {
                if (!AppendCharacter && !DisAppendCharacter) return 0;
                var timeValue = caracterVO.Command.CommandValues.FirstOrDefault(cv => cv.CommandValueType == CommandValueType.TIME);
                if (timeValue == null){
                    return 0;
                }
                return int.Parse(timeValue.Value);
            }
        }
        bool isAnimating = false;
        public bool IsAnimating => isAnimating;

        public void SetCharacterVO(ICaracterVO caracterVO)
        {
            this.caracterVO = caracterVO;
            this.gameObject.SetActive(true);
            CharacterBodyImage.sprite = caracterVO.BodySprite;
            CharacterBodyImage.SetNativeSize();
            CharacterBodyImage.transform.localPosition = caracterVO.BodyPostion;

            CharacterFaceImage.sprite = caracterVO.FaceSprite;
            CharacterFaceImage.SetNativeSize();
            CharacterFaceImage.transform.localPosition = caracterVO.FacePostion;

        }
        public void AppendEffect()
        {
            if (isShowed) return;
            float time = (float)effectTime * 0.001f;
            isAnimating = true;
            //実装！
            iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", time, "onupdate", "SetViewAlpha", "oncomplete", "CompleteAnimation","oncompletetarget", this.gameObject));
            isShowed = true;
        }

        public void DisAppendEffect()
        {
            if (!isShowed) return;
            float time = (float)effectTime * 0.001f;
            isAnimating = true;
            //実装！
            iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", time, "onupdate", "SetViewAlpha", "oncomplete", "CompleteAnimation","oncompletetarget", this.gameObject ));

            isShowed = false;
        }
        void SetViewAlpha(float alpha)
        {
            // iTweenで呼ばれたら、受け取った値をImageのアルファ値にセット
            canvasGroup.alpha = alpha;
        }
        void CompleteAnimation()
        {
            isAnimating = false;
        }

    }
}