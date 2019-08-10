using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.EscapeFramework
{
    public interface IItemWindowView
    {
        void Hide();
        void Show();
    }

    public class ItemWindowView : MonoBehaviour,IView, IItemWindowView
    {
        [SerializeField]
        ItemColumnView itemColumnViewPrefab;
        [SerializeField]
        ItemColumnView emptyItemColumnViewPrefab;

        List<ItemColumnView> itemColumnViews = new List<ItemColumnView>();
        [SerializeField]
        GridLayoutGroup grid;

        void SetItems(List<IItemSpriteVO> itemSpriteVOs)
        {
            int emptyItemCount = this.GetController<EscapeGameController>().GetEscapeGameDefins().MAX_ITEM_LIST_COUNT - itemSpriteVOs.Count;

            itemColumnViews.ForEach(it => Destroy(it.gameObject));
            itemColumnViews.Clear();
            itemSpriteVOs.ForEach(it =>
            {
                var icv = PrefabFolder.InstantiateTo<ItemColumnView>(itemColumnViewPrefab, grid.transform);
                icv.Initialize(it);
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
            //ゲームコントローラーのユーザーアイテムリストチェンジ時のコールバックに応じてアイテムを生成する処理
            this.GetController<EscapeGameController>().AddUserItemListChangeCallBack((items) => {
                SetItems(items);
            });
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
