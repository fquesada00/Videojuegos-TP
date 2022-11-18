using System;
using System.Collections.Generic;
using Strategies;
using UnityEngine;

namespace Controllers
{
    public class SoundController : MonoBehaviour, IListenable
    {
        [SerializeField] private AudioSource _audioSource;
        public AudioSource AudioSource => _audioSource;
        
        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;

        [SerializeField] private float _volume;
        
        [SerializeField] private List<SoundAudioClip> _soundAudioClips;

        private void Start()
        {
            InitAudioSource();
        }


        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayOnShot(SoundType soundType)
        {
            SoundAudioClip soundAudioClip = GetSoundAudioClip(soundType); 
            if(soundAudioClip != null && _audioSource != null)
                _audioSource.PlayOneShot(soundAudioClip.AudioClip, soundAudioClip.Volume);
        }
        
        private SoundAudioClip GetSoundAudioClip(SoundType soundType)
        {
            foreach (var soundAudioClip in _soundAudioClips)
            {
                if (soundAudioClip.SoundType == soundType)
                {
                    return soundAudioClip;
                }
            }
            
            return null;
        }

    }

    public enum SoundType
    {
        ATTACK,
        DEATH,
        HIT,
        JUMP,
        RUN,
        VICTORY,
        DEFEAT,
        WHISPER
    }
    
    [System.Serializable]
    public class SoundAudioClip
    {
        [SerializeField] private SoundType _soundType;
        public SoundType SoundType => _soundType;
        
        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;
        
        [SerializeField] private float _volume;
        public float Volume => _volume;
        
    }
}