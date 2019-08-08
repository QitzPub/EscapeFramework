using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qitz.EscapeFramework;

namespace Qitz.EscapeFramework
{
    [CreateAssetMenu]
    public class EscapeGameAudioDataStore : ADataStore, IEscapeGameAudioDataStore
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

    public interface IEscapeGameAudioDataStore
    {
        List<QitzAudioAsset> QitzAudios { get; }
        List<QitzAudioAsset> QitzSEs { get; }
    }


}