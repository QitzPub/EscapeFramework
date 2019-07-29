using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IItemDataStore
    {
        List<IItemSpriteVO> Items { get; }
        IItemSpriteVO GetItemFromItemName(ItemName itemName);
    }
    public class ItemDataStore : ADataStore, IItemDataStore
    {
        [SerializeField]
        List<ItemSpriteVO> items;
        public List<IItemSpriteVO> Items => items.Select(it => (IItemSpriteVO)it).ToList();
        public IItemSpriteVO GetItemFromItemName(ItemName itemName)
        {
            return Items.FirstOrDefault(isv=>isv.ItemName==itemName);
        }
    }
}