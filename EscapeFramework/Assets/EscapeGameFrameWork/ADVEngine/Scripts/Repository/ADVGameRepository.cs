
using System.Collections.Generic;
using Qitz.ArchitectureCore.ADVGame;
using UnityEngine;

namespace Qitz.ADVGame
{
    public interface IADVGameRepository
    {
        void Initialize(string macro);
        void ReLoad(string macro);
        List<ICutVO> CutVOs { get; }
    }
    public class ADVGameRepository : ARepository, IADVGameRepository
    {
        [SerializeField]
        ADVGameDataStore aDVGameDataStore;

        //public IADVGameDataStore ADVGameDataStore => aDVGameDataStore;

        public List<ICutVO> CutVOs => aDVGameDataStore.CutVOs;

        [SerializeField]
        ADVSpriteDataStore aDVSpriteDataStore;
        [SerializeField]
        ADVAudioDataStore aDVAudioDataStore;

        public void Initialize(string macro)
        {
            this.aDVGameDataStore.Initialize(macro);
            aDVGameDataStore.SetDataStore(aDVSpriteDataStore, aDVAudioDataStore);
        }

        public void ReLoad(string macro)
        {
            this.aDVGameDataStore.Initialize(macro);
            aDVGameDataStore.SetDataStore(aDVSpriteDataStore, aDVAudioDataStore);
        }
    }
}