using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    [SerializeField]
    private AudioConfig.SoundIDs soundID;

    public void PlaySound()
    {
        AudioController.Instance.PlaySound(soundID);
    }
}
