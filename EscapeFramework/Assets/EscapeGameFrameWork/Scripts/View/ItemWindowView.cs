using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public interface IItemWindowView
    {
        void Hide();
        void Show();
        void UseItem(ItemName itemName);
    }

    public class ItemWindowView : MonoBehaviour,IView, IItemWindowView
    {
        [SerializeField]
        ItemColumnView itemColumnViewPrefab;
        [SerializeField]
        ItemColumnView emptyItemColumnViewPrefab;
        [SerializeField]
        ItemDetailView itemDetailView;

        List<ItemColumnView> itemColumnViews = new List<ItemColumnView>();
        [SerializeField]
        GridLayoutGroup grid;
        List<IItemDataVO> currentItems = new List<IItemDataVO>();

        void SetItems(List<IItemDataVO> itemSpriteVOs)
        {
            int emptyItemCount = this.GetController<EscapeGameController>().GetEscapeGameDefins().MAX_ITEM_LIST_COUNT - itemSpriteVOs.Count;

            itemColumnViews.ForEach(it => Destroy(it.gameObject));
            itemColumnViews.Clear();
            itemSpriteVOs.ForEach(it =>
            {
                var icv = PrefabFolder.InstantiateTo<ItemColumnView>(itemColumnViewPrefab, grid.transform);
                icv.Initialize(it, DisSelectDisplay);
                itemColumnViews.Add(icv);
            });
            for (int i = 0; i < emptyItemCount; i++)
            {
                var icv = PrefabFolder.InstantiateTo<ItemColumnView>(emptyItemColumnViewPrefab, grid.transform);
                itemColumnViews.Add(icv);
            }
        }
        void Awake()
        {
            itemDetailView.Close();
            //ゲームコントローラーのユーザーアイテムリストチェンジ時のコールバックに応じてアイテムを生成する処理
            this.GetController<EscapeGameController>().AddUserItemListChangeCallBack((items) => {
                if (items.Count != 0 && currentItems.SequenceEqual(items))
                {
                    return;
                }
                Debug.Log("AddUserItemListChangeCallBack");
                SetItems(items);
                currentItems = items;
            });
        }
        public void UseItem(ItemName itemName)
        {
            var controller = this.GetController<EscapeGameController>();
            IItemDataVO itemData;
            //TODO ネスト深いのでなおす
            if (itemDetailView.IsOpened)
            {
                //ここでアイテム合成判定を行う
                var synthesizedItemName = controller.SynthesizeItems(itemDetailView.CurrentItem.ItemName, itemName);
                if(synthesizedItemName != ItemName.NONE)
                {
                    itemData = currentItems.FirstOrDefault(ci => ci.ItemName == synthesizedItemName);
                    itemDetailView.Open(itemData);
                    return;
                }
            }

            itemData = currentItems.FirstOrDefault(ci => ci.ItemName == itemName);
            itemDetailView.Open(itemData);
        }

        public void DisSelectDisplay()
        {
            itemColumnViews.ForEach(iv=>iv.DisSelectDisplay());
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}
