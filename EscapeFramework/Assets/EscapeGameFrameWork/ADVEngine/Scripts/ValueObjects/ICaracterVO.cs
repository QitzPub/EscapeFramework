
using UnityEngine;

namespace Qitz.ADVGame
{
    public interface ICaracterVO
    {
        string Name { get; }

        Expression Expression { get; }
        Character Character { get; }
        Costume Costume { get; }

        string SpriteBodyName { get; }

        ICharacterBodySpriteVO CharacterBodySpriteVO { get; }
        ICharacterFaceSpriteVO FaceSpriteVO { get; }

        string SpriteFaceName { get; }

        Sprite BodySprite { get; }
        Sprite FaceSprite { get; }

        Vector2 BodyPostion { get; }
        Vector2 FacePostion { get; }

        ICommandWrapVO Command { get; }

        void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore);
        bool AppendCharacter { get; }
        bool DisAppendCharacter { get; }
        void UpDataCharacterStateFromNewCharacterVO(ICaracterVO newCharacterState);
    }

    ////キャラクタの出現効果
    //public enum CharacterEffectType
    //{
    //    NONE,    //なし（現状維持）
    //    UP,      //下からにゅい〜ん
    //    DOWN,    //上から
    //    SLIDEIN, //スライドイン
    //    FADEIN,  //フェードイン
        
    //    //ここから退出処理
    //    SLIDEOUT,//スライドアウト
    //    FADEOUT, //フェードアウト
        
    //}
}
