using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviorSingleton<AudioController>
{
    [SerializeField]
    private AudioConfig _audioConfig;


    private void Start()
    {
        
        SceneManager.sceneLoaded += onLoaded;

        
    }

    private void onLoaded(Scene arg0, LoadSceneMode arg1)
    {
        var AllAudios = GetComponents<AudioSource>();
        for (int i = AllAudios.Length - 1; i >= 0; i--)
        {
            Destroy(AllAudios[i]);
        }


        
    }

    public void PlaySound(AudioConfig.SoundIDs soundName)
    {
        AudioSource _audioSource = CreateAudioSource();


        for (int i = 0; i < _audioConfig.shortSounds.Count; i++)
        {
            if (_audioConfig.shortSounds[i].soundID != soundName)
            {
                continue;
            }
            int randomIndex = Random.Range(0, _audioConfig.shortSounds[i].audioClips.Length);
            _audioSource.volume = _audioConfig.shortSounds[i].volume;
            _audioSource.clip = _audioConfig.shortSounds[i].audioClips[randomIndex];
            _audioSource.Play();

            ClockService.Instance.AddTimer(_audioSource.clip.length, false, () => DestroyAudioSource(_audioSource));
                              
            return;

        }

     
    }

    
    
    private void DestroyAudioSource(AudioSource audioSource)
    {
       
        Destroy(audioSource);
    }


    private AudioSource CreateAudioSource()
    {
        AudioSource _audioSource = gameObject.AddComponent<AudioSource>();
        return _audioSource;
        //_audioSourceList.Add(_audioSource);
    }

    public void LoadBanksFMOD()
    {

        GetComponent<FMODUnity.StudioBankLoader>().Load();
    }
}
