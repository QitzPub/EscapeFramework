using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Qitz.ADVGame
{
    public class CharactersWrapView : ACharactersWrapView
    {


        [System.Serializable]
        struct CharacterViewPostionSetting
        {
            [SerializeField]
            Vector3 postion;
            public Vector3 Postion => postion;
            [SerializeField]
            Vector3 scale;
            public Vector3 Scale => scale;
        }


        [SerializeField]
        List<CharacterView> characterViews;
        [SerializeField]
        List<CharacterViewPostionSetting> viewPostion;

        //List<ICaracterVO> prevAppendedCharacter = new List<ICaracterVO>();
        List<ICaracterVO> appendedCharacter = new List<ICaracterVO>();

        public override List<CharacterView> CharacterViews => characterViews;

        public override void SetCaracterVO(List<ICaracterVO> characters)
        {
            characters.ForEach(c=> SetAppendedCharacterList(ref appendedCharacter,c));
            SetViewPostionFromCharacterCount(appendedCharacter.Count);
            //一旦キャラクタービュウを非表示に
            characterViews.ForEach(cv => cv.gameObject.SetActive(false));

            for (int i = 0; i < appendedCharacter.Count; i++)
            {
                characterViews[i].SetCharacterVO(appendedCharacter[i]);
            }

            //ここでキャラクター消失フラグが入っているキャラクターを消すエフェクト
            characterViews.Where(cv => cv.DisAppendCharacter).ToList().ForEach(cv => cv.DisAppendEffect());
            //コマンドに出現コマンドが入っていたら出現エフェクトを入れる
            characterViews.Where(cv => cv.AppendCharacter).ToList().ForEach(cv => cv.AppendEffect());
            //消失フラグが入っているキャラクターをリストから取り除く
            characters.ForEach(c => RemoveDisAppendCharacter(ref appendedCharacter, c));

        }

        //キャラ数に応じて画面の表示倍率や位置を変える
        void SetViewPostionFromCharacterCount(int characterCount)
        {
            this.transform.localPosition = viewPostion[characterCount].Postion;
            this.transform.localScale = viewPostion[characterCount].Scale;
        }


        void SetAppendedCharacterList(ref List<ICaracterVO> _appendedCharacter, ICaracterVO newCharacterVO)
        {
            if (newCharacterVO.AppendCharacter)
            {
                _appendedCharacter.Add(newCharacterVO);
            }

            else
            {
                UpdateCharacterState(ref _appendedCharacter, newCharacterVO);
            }
        }

        void RemoveDisAppendCharacter(ref List<ICaracterVO> _appendedCharacter, ICaracterVO newCharacterVO)
        {
            if (newCharacterVO.DisAppendCharacter)
            {
                _appendedCharacter = _appendedCharacter.Where(ac => ac.Name != newCharacterVO.Name).ToList();
            }
        }

        void UpdateCharacterState(ref List<ICaracterVO> _appendedCharacter, ICaracterVO newCharacterVO)
        {
            foreach (var ac in _appendedCharacter)
            {
                if (ac.Name == newCharacterVO.Name)
                {
                    ac.UpDataCharacterStateFromNewCharacterVO(newCharacterVO);
                }
            }
        }

        public override void ClearCharacterCache()
        {
            appendedCharacter = new List<ICaracterVO>();
        }

    }
}
