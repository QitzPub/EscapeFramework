using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Qitz.EscapeFramework
{
    [RequireComponent(typeof(Button))]
    public class ItemColumnView : MonoBehaviour, IView
    {
        [SerializeField]
        Image itemImage;
        //[SerializeField]
        //ItemDragableView itemDragableView;
        IItemDataVO itemSpriteVO;
        public IItemDataVO ItemSpriteVO => itemSpriteVO;
        public Button Button => this.GetComponent<Button>();
        [SerializeField]
        Image selectImage;
        Action itemSelectAction;

        public void Initialize(IItemDataVO itemSpriteVO, Action itemSelectAction)
        {
            this.itemSelectAction = itemSelectAction;
            Button.onClick.RemoveAllListeners();
            this.itemSpriteVO = itemSpriteVO;
            itemImage.sprite = itemSpriteVO.Sprite;
            //itemDragableView.SetItemName(itemSpriteVO.ItemName);
            selectImage.gameObject.SetActive(false);
            Button.onClick.AddListener(SetSelect);
        }
        public void SetSelect()
        {
            //他のアイテムセレクト表示を解除
            itemSelectAction.Invoke();
            selectImage.gameObject.SetActive(true);
            var controller = this.GetController<EscapeGameController>();
            controller.SelectItem(itemSpriteVO.ItemName);
        }
        public void DisSelectDisplay()
        {
            selectImage.gameObject.SetActive(false);
        }

    }
}