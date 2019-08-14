using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Qitz.EscapeFramework
{
    public class ItemDragableView : MonoBehaviour,IView,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {

        [SerializeField]
        UnityEvent onBeginDrag;
        [SerializeField]
        UnityEvent onEndDrag;
        ItemName itemName;

        public void SetItemName(ItemName itemName)
        {
            this.itemName = itemName;
        }

        Vector3 dragStartPostion;
        public void OnBeginDrag(PointerEventData eventData)
        {
            dragStartPostion = this.transform.position;
            onBeginDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            this.transform.position = eventData.position;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var hit in raycastResults)
            {
                var itemDropable = hit.gameObject.GetComponent<ItemDropable>();
                if(itemDropable != null)
                {
                    itemDropable.DropAction?.Invoke(itemName);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.transform.position = dragStartPostion;
            onEndDrag?.Invoke();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

    }
}
