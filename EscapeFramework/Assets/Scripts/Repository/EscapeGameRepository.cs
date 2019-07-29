
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IEscapeGameRepository
    {
        IItemDataStore ItemDataStore { get; }
        IEscapeGameUserDataStore EscapeGameUserDataStore { get; }
        IEscapeGameDefinsDataStore EscapeGameDefinsDataStore { get; }
    }
    //[CreateAssetMenu]
    public class EscapeGameRepository : ARepository, IEscapeGameRepository
    {

        [SerializeField]
        ItemDataStore itemDataStore;
        public IItemDataStore ItemDataStore => itemDataStore;

        EscapeGameUserDataStore escapeGameUserDataStore;
        public IEscapeGameUserDataStore EscapeGameUserDataStore => escapeGameUserDataStore;

        [SerializeField]
        EscapeGameDefinsDataStore escapeGameDefinsDataStore;
        public IEscapeGameDefinsDataStore EscapeGameDefinsDataStore => escapeGameDefinsDataStore;

        //ユーザーが持っている持ち物のアイテムSpriteList
        public List<IItemSpriteVO> UserPossessionItemSpriteList => escapeGameUserDataStore.Items.Select(iv => itemDataStore.GetItemFromItemName(iv.ItemName)).ToList();
        //ゲーム中の全アイテムSpriteList
        public List<IItemSpriteVO> AllItemSpriteList => itemDataStore.Items;

        public void Initialize()
        {
            escapeGameUserDataStore = new EscapeGameUserDataStore();
        }

    }

}