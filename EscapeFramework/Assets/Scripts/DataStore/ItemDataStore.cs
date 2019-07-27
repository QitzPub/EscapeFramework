using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IItemDataStore
    {
        List<IItemVO> Items { get; }
    }
    public class ItemDataStore : ADataStore, IItemDataStore
    {
        [SerializeField]
        List<ItemVO> items;
        public List<IItemVO> Items => items.Select(it => (IItemVO)it).ToList();
    }
}