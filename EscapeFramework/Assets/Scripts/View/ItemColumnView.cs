using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public class ItemColumnView : MonoBehaviour, IView
    {
        [SerializeField]
        Image itemImage;
        IItemSpriteVO itemSpriteVO;
        public IItemSpriteVO ItemSpriteVO => itemSpriteVO;

        public void Initialize(IItemSpriteVO itemSpriteVO)
        {
            this.itemSpriteVO = itemSpriteVO;
            itemImage.sprite = itemSpriteVO.Sprite;
        }


    }
}