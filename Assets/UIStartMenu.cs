using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject mainMenu;


    private bool audioResumed = false;


    private void Start()
    {
        AudioController.Instance.PlaySound(AudioConfig.SoundIDs.startmusic);
    }

    public void CallMainMenu()
    {
        ActivateFMOD();
        startMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    private void ActivateFMOD()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            audioResumed = true;
        }

        AudioController.Instance.LoadBanksFMOD();
    }
}
