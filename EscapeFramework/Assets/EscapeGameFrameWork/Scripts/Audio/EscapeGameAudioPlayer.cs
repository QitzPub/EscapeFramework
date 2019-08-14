using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Qitz.EscapeFramework
{
    public class EscapeGameAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        AudioSource bgmAudioSource;
        [SerializeField]
        AudioSource seAudioSource;
        IEscapeGameAudioDataStore escapeGameAudioDataStore;

        public void Initialize(IEscapeGameAudioDataStore escapeGameAudioDataStore)
        {
            this.escapeGameAudioDataStore = escapeGameAudioDataStore;
        }

        public void PlayAudio(BGMName bgmName)
        {
            var audio = escapeGameAudioDataStore.QitzAudios.FirstOrDefault(qs => qs.Audio.name == bgmName.ToString()).Audio;
            PlayAudio(audio);
        }
        public void PlayAudio(AudioClip audioClip)
        {
            if (audioClip == null) return;
            bgmAudioSource.clip = audioClip;
            bgmAudioSource.Play();
        }

        public void PlaySE(SEName sEName)
        {
            var audio = escapeGameAudioDataStore.QitzSEs.FirstOrDefault(qs=>qs.Audio.name == sEName.ToString()).Audio;
            PlaySE(audio);
        }

        public void PlaySE(AudioClip audioClip)
        {
            if (audioClip == null) return;
            seAudioSource.PlayOneShot(audioClip);
        }
    }
}