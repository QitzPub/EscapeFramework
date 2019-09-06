using System.Collections.Generic;
using UnityEngine;
using Qitz.ArchitectureCore.ADVGame;
using Qitz.ADVGame.ParseUtil;

namespace Qitz.ADVGame
{
    public interface IADVGameDataStore
    {
        List<ICutVO> CutVOs { get; }
        void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore, IADVAudioDataStore aDVAudioDataStore);
    }

    //[CreateAssetMenu]
    public class ADVGameDataStore : ADataStore, IADVGameDataStore
    {

        public void Initialize(string macro)
        {
            QitzADVMacroParseUtil util = new QitzADVMacroParseUtil(macro);
            CutVOs = util.Deserialize();
        }

        public List<ICutVO> CutVOs { get; private set; }

        public void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore, IADVAudioDataStore aDVAudioDataStore)
        {
            CutVOs.ForEach(cv => cv.SetDataStore(aDVSpriteDataStore, aDVAudioDataStore));
        }
        
        
    }
}