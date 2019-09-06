
using UnityEngine;
using System.Linq;
using System.IO;

namespace Qitz.ADVGame
{
    public class BackgroundVO : IBackgroundVO
    {
        IADVSpriteDataStore aDVSpriteDataStore;

        //public string Name { get; set; }

        public BackgroundVO(string spriteBackGroundName)
        {
            SpriteBackGroundName = spriteBackGroundName;
        }

        string withOutExtentionName => Path.GetFileNameWithoutExtension(SpriteBackGroundName);

        public string SpriteBackGroundName { get; private set; }

        public Sprite SpriteBackGround => aDVSpriteDataStore.BackgroundSpriteList.FirstOrDefault(bs=>bs.Sprite.name== withOutExtentionName).Sprite;

        public void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore)
        {
            this.aDVSpriteDataStore = aDVSpriteDataStore;
        }
    }
}
