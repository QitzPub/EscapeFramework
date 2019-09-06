using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qitz.ArchitectureCore.ADVGame;
using System;

namespace Qitz.ADVGame
{
    //[CreateAssetMenu]
    public class ADVAudioDataStore : ADataStore, IADVAudioDataStore
    {
        [SerializeField]
        List<QitzAudioAsset> qitzAudios;
        public List<QitzAudioAsset> QitzAudios => qitzAudios;
        [SerializeField]
        List<QitzAudioAsset> qitzSEs;
        public List<QitzAudioAsset> QitzSEs => qitzSEs;
    }

    [Serializable]
    public class QitzAudioAsset
    {
        [SerializeField]
        AudioClip audio;
        public AudioClip Audio => audio;
    }

    public interface IADVAudioDataStore
    {
        List<QitzAudioAsset> QitzAudios { get; }
        List<QitzAudioAsset> QitzSEs { get; }
    }


}