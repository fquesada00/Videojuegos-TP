using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        //Random sort
        _audioClips.Sort((a, b) => Random.Range(-1, 1));

        StartCoroutine(PlaySoundtrack());
    }

    private IEnumerator PlaySoundtrack()
    {
        while (true)
        {
            foreach (var audioClip in _audioClips)
            {
                _audioSource.clip = audioClip;
                _audioSource.Play();
                yield return new WaitForSeconds(audioClip.length);
            }
        }
    }


}
