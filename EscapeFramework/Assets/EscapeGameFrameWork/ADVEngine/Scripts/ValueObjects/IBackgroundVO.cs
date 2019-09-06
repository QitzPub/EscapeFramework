
using UnityEngine;
using UnityEngine.UI;

namespace Qitz.ADVGame
{
    public interface IBackgroundVO
    {
        //string Name { get; }

        string SpriteBackGroundName { get; }

        Sprite SpriteBackGround { get; }

        void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore);

    }
}
