using System;
using System.Collections.Generic;
using UniRx.Async;

namespace Qitz.ADVGame
{
    public abstract class AChoiceSelectView:ADVGameView
    {
        abstract public void Initialize(Action<string> selectAction, List<ICommandWrapVO> commands);
        abstract public UniTask HideView();
        abstract public void HideImmediately();
        abstract public List<SelectItemView> SelectItems { get; set; }
    }
}