using System.Collections.Generic;
namespace Qitz.ADVGame
{
    public abstract class ABackgroundView : ADVGameView
    {
        abstract public void SetBackgroundVO(IBackgroundVO vo);
        abstract public void SetEffect(List<ICommandWrapVO> vo);

    }
}
