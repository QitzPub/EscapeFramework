using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx.Async;
using UniRx;
using UniRx.Triggers;

namespace Qitz.ADVGame
{
    public abstract class ASelectItemView: ADVGameView
    {
        abstract public void Initialize(List<CommandVO> selectCommands, Action<string> selectAction);
        abstract public void HideView();
        abstract public void ShowView();
        abstract public void DisableButton();
    }

    public class SelectItemView : ASelectItemView
    {
        [SerializeField]
        Text text;
        [SerializeField]
        Button button;
        [SerializeField]
        iTweenAnimation[] showAnimations;
        [SerializeField]
        iTweenAnimation hideAnimation;
        public bool IsSelected { get; private set; }
        public bool IsAnimating => (showAnimations.Any(sa => sa.IsAnimating) || hideAnimation.IsAnimating) && this.gameObject.activeSelf;

        public async override void HideView()
        {
            hideAnimation.DoTween();
            await this.UpdateAsObservable().Where(_ => !hideAnimation.IsAnimating || !this.gameObject.activeSelf).Take(1);
            //this.gameObject.SetActive(false);
            IsSelected = false;
        }
        public override void ShowView()
        {
            this.gameObject.SetActive(true);
            foreach (var animation in showAnimations)
            {
                animation.DoTween();
            }
        }

        public override void Initialize(List<CommandVO> selectCommands, Action<string> selectAction)
        {
            this.text.text = selectCommands.FirstOrDefault(sc=>sc.CommandValueType == CommandValueType.TEXT).Value;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=> {
                IsSelected = true;
                var selectValue = selectCommands.FirstOrDefault(sc => sc.CommandValueType == CommandValueType.TARGET).Value;
                selectAction.Invoke(selectValue);

            });
            button.enabled = true;
            this.gameObject.SetActive(true);
        }

        public override void DisableButton()
        {
            button.enabled = false;
        }
    }
}
