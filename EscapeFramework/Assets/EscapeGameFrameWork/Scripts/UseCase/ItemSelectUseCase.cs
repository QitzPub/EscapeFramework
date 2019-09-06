using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.EscapeFramework
{
    public interface IItemSelectUseCase
    {
        ItemName SelectedItem { get; }
        void SelectItem(ItemName item);
        IItemWindowView ItemWindowView { get; }
        void DisSelectItem();
    }

    public class ItemSelectUseCase: IItemSelectUseCase
    {
        ItemName selectedItem;
        public ItemName SelectedItem => selectedItem;
        ItemWindowView itemWindowView;
        public IItemWindowView ItemWindowView => itemWindowView;

        public ItemSelectUseCase(ItemWindowView itemWindowView)
        {
            this.itemWindowView = itemWindowView;
        }

        public void SelectItem(ItemName item)
        {
            if (selectedItem == item)
            {
                UseItem(item);
            }
            selectedItem = item;
        }

        public void DisSelectItem()
        {
            selectedItem = ItemName.NONE;
            itemWindowView.DisSelectDisplay();
        }

        void UseItem(ItemName item)
        {
            ItemWindowView.UseItem(item);
            Debug.Log("UseItem");
        }
    }
}