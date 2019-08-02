
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
        List<CountEventSetting> CountEventSettings { get; }
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

        [SerializeField]
        CountEventDataStore countEventDataStore;

        //ユーザーが持っている持ち物のアイテムSpriteList
        public List<IItemSpriteVO> UserPossessionItemSpriteList => escapeGameUserDataStore.Items.Select(iv => itemDataStore.GetItemFromItemName(iv.ItemName)).ToList();
        //ゲーム中の全アイテムSpriteList
        public List<IItemSpriteVO> AllItemSpriteList => itemDataStore.Items;
        //カウントダウンイベント用
        public List<CountEventSetting> CountEventSettings => countEventDataStore.CountEventSettings;

        public void Initialize()
        {
            escapeGameUserDataStore = new EscapeGameUserDataStore(CountEventSettings);
        }

    }

}