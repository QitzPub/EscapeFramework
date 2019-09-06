using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public class ItemDetailView : MonoBehaviour, IView
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        [SerializeField]
        Image itemImage;
        [SerializeField]
        Text itemDescription;
        bool isOpened = false;
        [SerializeField]
        public bool IsOpened => isOpened;
        IItemDataVO currentItem;
        public IItemDataVO CurrentItem => currentItem;

        public void Open(IItemDataVO item)
        {
            this.gameObject.SetActive(true);
            itemImage.sprite = item.Sprite;
            itemDescription.text = item.ItemDescription;
            this.currentItem = item;
            isOpened = true;
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            isOpened = false;
        }
    }
}