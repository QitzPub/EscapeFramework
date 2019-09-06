using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IItemDataStore
    {
        List<IItemDataVO> Items { get; }
        IItemDataVO GetItemFromItemName(ItemName itemName);
    }
    public class ItemDataStore : ADataStore, IItemDataStore
    {
        [SerializeField]
        List<ItemDataVO> items;
        public List<IItemDataVO> Items => items.Select(it => (IItemDataVO)it).ToList();
        public IItemDataVO GetItemFromItemName(ItemName itemName)
        {
            return Items.FirstOrDefault(isv=>isv.ItemName==itemName);
        }
    }
}