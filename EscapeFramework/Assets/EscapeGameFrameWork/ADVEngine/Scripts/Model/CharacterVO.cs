using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.ADVGame
{
    public class CharacterVO : ICaracterVO
    {
        public CharacterVO(ICommandWrapVO commandWrapVO)
        {
            string characterName = commandWrapVO.CommandHeadVO.CommandValue;
            var bodyCommandValue = commandWrapVO.CommandValues.FirstOrDefault(cv => cv.CommandValueType == CommandValueType.SET_COSTUME);
            string bodyName = bodyCommandValue == null ? "" : bodyCommandValue.Value;
            var faceCommandValue = commandWrapVO.CommandValues.FirstOrDefault(cv => cv.CommandValueType == CommandValueType.SET_FACE);
            string faceName = faceCommandValue == null ? "" : faceCommandValue.Value;

            this.Name = characterName;
            this.SpriteBodyName = bodyName;
            this.SpriteFaceName = faceName;
            //this.characterCommands = commandWrapVO.CommandValues;
            this.Command = commandWrapVO;
        }

        IADVSpriteDataStore aDVSpriteDataStore;

        //List<CommandVO> characterCommands;

        public bool ExistFaseSprite => Command.CommandValues.FirstOrDefault(cv => cv.CommandValueType == CommandValueType.SET_FACE) != null && SpriteFaceName != "";
        public bool ExistBodySprite => Command.CommandValues.FirstOrDefault(cv => cv.CommandValueType == CommandValueType.SET_COSTUME) != null && SpriteBodyName != "";

        public string Name { get; private set; }

        public Expression Expression => (Expression)Enum.Parse(typeof(Expression), SpriteFaceName.Replace("(", "").Replace(")", ""), true);

        public string SpriteBodyName { get; private set; }

        public string SpriteFaceName { get; private set; }


        public Character Character => (Character)Enum.Parse(typeof(Character), Name, true);

        public Costume Costume
        {
            get
            {
                if (SpriteBodyName == null) return Costume.NONE;
                return (Costume)Enum.Parse(typeof(Costume), SpriteBodyName.Replace("(", "").Replace(")", ""), true);
            }
        }


        public ICharacterBodySpriteVO CharacterBodySpriteVO => aDVSpriteDataStore.BodySpriteList.FirstOrDefault(bs => bs.Character == this.Character && bs.Costume == this.Costume);

        public ICharacterFaceSpriteVO FaceSpriteVO => aDVSpriteDataStore.FaceSpriteList.FirstOrDefault(fs => fs.Character == this.Character && fs.Expression == this.Expression);

        public Sprite BodySprite {
            get {
                if (!ExistBodySprite) return null;

                var bs = this.aDVSpriteDataStore.BodySpriteList.FirstOrDefault(b => b.Character == Character && b.Costume == Costume);
                if (bs == null)
                {
                    return this.aDVSpriteDataStore.BodySpriteList.FirstOrDefault(b => b.Character == Character).Sprite;
                }
                return bs.Sprite;
            }
        }
        public Sprite FaceSprite
        {
            get
            {
                if (!ExistFaseSprite) return null;

                var fs = this.aDVSpriteDataStore.FaceSpriteList.FirstOrDefault(f => f.Character == Character && f.Expression == Expression);
                return fs.Sprite;
            }
        }

        public Vector2 BodyPostion => aDVSpriteDataStore.CharacterBodyPostionList.FirstOrDefault(cbp => cbp.Character == Character).Postion;

        public Vector2 FacePostion => aDVSpriteDataStore.CharacterFacePostionList.FirstOrDefault(cfp => cfp.Character == Character).Postion;

        public bool AppendCharacter => Command.CommandValues.FirstOrDefault(cc => cc.CommandValueType == CommandValueType.APPEAR) != null;

        public bool DisAppendCharacter => Command.CommandValues.FirstOrDefault(cc => cc.CommandValueType == CommandValueType.DISAPPEAR) != null;

        public ICommandWrapVO Command { get; private set; }



        //public void SetCharacterCommands(List<CommandVO> characterCommands)
        //{
        //    this.characterCommands = characterCommands;
        //}

        public void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore)
        {
            this.aDVSpriteDataStore = aDVSpriteDataStore;
        }

        public void UpDataCharacterStateFromNewCharacterVO(ICaracterVO newCharacterState)
        {
            this.Name = newCharacterState.Name;
            this.SpriteBodyName = newCharacterState.SpriteBodyName == "" ? this.SpriteBodyName : newCharacterState.SpriteBodyName;
            this.SpriteFaceName = newCharacterState.SpriteFaceName == "" ? this.SpriteFaceName : newCharacterState.SpriteFaceName;
            //キャラクターの消失コマンドもマージする
            var disApperCommand = newCharacterState.Command.CommandValues.FirstOrDefault(cc => cc.CommandValueType == CommandValueType.DISAPPEAR);
            if(disApperCommand != null && !DisAppendCharacter)
            {
                Command.CommandValues.Add(disApperCommand);

            }
            //キャラの出現コマンドを取り除く
            var apperCommand = Command.CommandValues.FirstOrDefault(cc => cc.CommandValueType == CommandValueType.APPEAR);
            if(apperCommand != null)
            {
                Command.CommandValues.Remove(apperCommand);
            }
        }
    }
}