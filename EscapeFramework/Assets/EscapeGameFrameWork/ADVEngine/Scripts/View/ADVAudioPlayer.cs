using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Qitz.ADVGame
{
    public class ADVAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        AudioSource bgmAudioSource;
        [SerializeField]
        AudioSource seAudioSource;

        public void PlayAudio(AudioClip audioClip)
        {
            if (audioClip == null) return;
            bgmAudioSource.clip = audioClip;
            bgmAudioSource.Play();
        }
        public void PlaySE(AudioClip audioClip)
        {
            if (audioClip == null) return;
            seAudioSource.PlayOneShot(audioClip);
        }
    }
}