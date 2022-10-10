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

        [SerializeField] private float _volume = 1f;
        
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
            AudioClip audioClip = GetAudioClip(soundType); 
            
            if(audioClip != null)
                _audioSource.PlayOneShot(audioClip, _volume);
        }
        
        private AudioClip GetAudioClip(SoundType soundType)
        {
            foreach (var soundAudioClip in _soundAudioClips)
            {
                if (soundAudioClip.SoundType == soundType)
                {
                    return soundAudioClip.AudioClip;
                }
            }
            
            Debug.LogError("Sound " + soundType + " not found!");
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
        DEFEAT
    }
    
    [System.Serializable]
    public class SoundAudioClip
    {
        [SerializeField] private SoundType _soundType;
        public SoundType SoundType => _soundType;
        
        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;
        
    }
}