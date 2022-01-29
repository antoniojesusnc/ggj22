using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviorSingleton<AudioController>
{
    [SerializeField]
    private AudioConfig _audioConfig;

    [SerializeField]
    private AudioSource _audioSource;

    
    
    public void PlaySound(AudioConfig.SoundIDs soundName)
    {
        
        for (int i = 0; i < _audioConfig.shortSounds.Count; i++)
        {
            if (_audioConfig.shortSounds[i].soundID != soundName)
            {
                continue;
            }
            int randomIndex = Random.Range(0, _audioConfig.shortSounds[i].audioClips.Length);
            _audioSource.clip = _audioConfig.shortSounds[i].audioClips[randomIndex];
            _audioSource.Play();
            return;
        }
    }


}
