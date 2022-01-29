using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Audio/new Audio Config", order = 1)]
public class AudioConfig : ScriptableObject
{

    public enum SoundIDs
    {
        coin,
        collision,
        gameover
    }
    public List<shortSoundsConfig> shortSounds;

    
    [System.Serializable]
    public class shortSoundsConfig
    {
        public SoundIDs soundID;
        public AudioClip audioClip;

    }


}
