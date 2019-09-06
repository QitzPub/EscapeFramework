
using System.Collections.Generic;

namespace Qitz.ADVGame
{
    public interface ICutVO
    {
        int Number { get; }

        IWindowVO WindowVO { get; }

        string SelTagValue { get; }
        string BGMValue { get; }
        string BGValue { get; }
        string SEValue { get; }
        string JumpToValue { get; }
        //string BgmID { get; }
        QitzAudioAsset QitzAudio { get; }
        QitzAudioAsset SE { get; }
        List<ICommandWrapVO> Commands { get; }

        IBackgroundVO BackgroundVO { get; }

        List<ICaracterVO> Caracters { get; }

        List<IChoiceVO> Choices { get; }

        void SetDataStore(IADVSpriteDataStore aDVSpriteDataStore, IADVAudioDataStore aDVAudioDataStore);

    }

}
