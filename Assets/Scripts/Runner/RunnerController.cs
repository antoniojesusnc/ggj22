using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class RunnerController : MonoBehaviour
{
    [SerializeField] private RunnerConfig _runnerConfig;
    
    public RunnerConfig RunnerConfig => _runnerConfig;

    public int CurrentTrack { get; private set; }
    
    private InputManager _inputManager;

    public event Action OnTrackChanged;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _inputManager = new InputManager(_runnerConfig, OnInput);

        var trackPositions = TrackManager.Instance.GetTrackPositions();
        var runnerGrahics = GetComponentsInChildren<RunnerGraphic>();
        for (int i = 0; i < runnerGrahics.Length; i++)
        {
            runnerGrahics[i].Init(this, i, trackPositions[i], i == TrackController.Track01);
        }
    }

    private void OnInput()
    {
        ChangeTrack();
    }

    private void ChangeTrack()
    {
        CurrentTrack = CurrentTrack == TrackController.Track01
            ? TrackController.Track02
            : TrackController.Track01;

        OnTrackChanged?.Invoke();
    }
}
