using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UniRx.Async;
using UniRx.Triggers;
using UniRx;

namespace Qitz.ADVGame
{
    public class ChoiceSelectView : AChoiceSelectView
    {
        [SerializeField]
        List<SelectItemView> selectItems;
        [SerializeField]
        Image background;

        public override List<SelectItemView> SelectItems { get => selectItems; set => selectItems = value; }

        public override void Initialize(Action<string> selectAction,List<ICommandWrapVO> commands)
        {
            SelectItems.ForEach(si => si.gameObject.SetActive(false));
            int i = 0;
            background.gameObject.SetActive(true);
            SelectItems.Take(commands.Count).ToList().ForEach(si=>si.ShowView());
            foreach (var c in commands)
            {
                SelectItems[i].Initialize(c.CommandValues, selectAction);
                i++;
            }
        }

        public async override UniTask HideView()
        {
            SelectItems.ForEach(si=>si.DisableButton());
            SelectItems.Where(si=>!si.IsSelected).ToList().ForEach(si=>si.HideView());
            await UniTask.Delay(2000);
            SelectItems.FirstOrDefault(si => si.IsSelected).HideView();
            await this.UpdateAsObservable().Where(_ => SelectItems.All(si => !si.IsAnimating)).Take(1);
            await UniTask.Delay(200);
            background.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        public override void HideImmediately()
        {
            SelectItems.ForEach(si => si.gameObject.SetActive(false));
            background.gameObject.SetActive(false);
        }
    }
}
