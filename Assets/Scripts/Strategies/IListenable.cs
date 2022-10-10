using Controllers;
using UnityEngine;

namespace Strategies
{
    public interface IListenable
    {
        AudioClip AudioClip { get; }
        AudioSource AudioSource { get; }

        void InitAudioSource();
        void PlayOnShot(SoundType soundType);
        // void Play();
        // void Stop();
    }
}