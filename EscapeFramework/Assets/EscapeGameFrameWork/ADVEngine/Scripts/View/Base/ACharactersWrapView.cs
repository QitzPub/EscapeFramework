
using System.Collections.Generic;

namespace Qitz.ADVGame
{
    public abstract class ACharactersWrapView:ADVGameView
    {
        abstract public void SetCaracterVO(List<ICaracterVO> characters);
        abstract public List<CharacterView> CharacterViews { get; }
        abstract public void ClearCharacterCache();
    }
}
