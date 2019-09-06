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

        public void Open(IItemDataVO item)
        {
            this.gameObject.SetActive(true);
            itemImage.sprite = item.Sprite;
            itemDescription.text = item.ItemDescription;
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}