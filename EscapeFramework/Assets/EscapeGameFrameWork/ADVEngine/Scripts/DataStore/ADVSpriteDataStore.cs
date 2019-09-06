using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qitz.ArchitectureCore.ADVGame;
using  System;
using  System.Linq;

namespace Qitz.ADVGame
{
    public class ADVSpriteDataStore : ADataStore,IADVSpriteDataStore
    {
        public List<ICharacterBodySpriteVO> BodySpriteList =>
            bodySpriteList.Select(bs => (ICharacterBodySpriteVO) bs).ToList();
        [SerializeField] private List<CharacterBodySpriteVO> bodySpriteList;

        public List<ICharacterFaceSpriteVO> FaceSpriteList =>
            faceSpriteList.Select(fs => (ICharacterFaceSpriteVO) fs).ToList();
        [SerializeField] private List<CharacterFaceSpriteVO> faceSpriteList;
        public List<IADVSpriteVO> BackgroundSpriteList => backgroundSpriteList.Select(bs=>(IADVSpriteVO)bs).ToList();

        public List<CharacterPostionVO> CharacterBodyPostionList => characterBodyPostionList;

        public List<CharacterPostionVO> CharacterFacePostionList => characterFacePostionList;

        [SerializeField] private List<ADVSpriteVO> backgroundSpriteList;

        [SerializeField] List<CharacterPostionVO> characterBodyPostionList;
        [SerializeField] List<CharacterPostionVO> characterFacePostionList;

    }

    public interface ICharacterBodySpriteVO : IADVSpriteVO
    {
        Character Character { get; }
        Costume Costume { get; }
    }

    [Serializable]
    public class CharacterPostionVO: AVO
    {
        [SerializeField] Vector2 postion;
        public Vector2 Postion => postion;
        [SerializeField] Character character;
        public Character Character => character;
    }

    [Serializable]
    public class CharacterBodySpriteVO : AVO,ICharacterBodySpriteVO
    {
        public Sprite Sprite => bodySprite;
        [SerializeField] private Sprite bodySprite;
        public Character Character => character;
        [SerializeField] private Character character;
        public Costume Costume => costume;
        [SerializeField] private Costume costume;
    }

    public interface ICharacterFaceSpriteVO:IADVSpriteVO
    {
        Character Character { get; }
        Expression Expression { get; }
    }
    
    [Serializable]
    public class CharacterFaceSpriteVO : AVO,ICharacterFaceSpriteVO
    {
        public Sprite Sprite => faceSprite;
        [SerializeField] private Sprite faceSprite;
        public Character Character => character;
        [SerializeField] private Character character;
        public Expression Expression => expression;
        [SerializeField] private Expression expression;
    }

    [Serializable]
    public class ADVSpriteVO : AVO, IADVSpriteVO
    {
        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite;
        
    }
    
}
