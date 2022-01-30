using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandicapController : MonoBehaviour
{
    [SerializeField] public float _handicap;
    public float Handicap => _handicap;

    private HandicapConfig _handicapConfig;

    private void Awake()
    {
        _handicapConfig = GameService.Instance.CurrentDifficulty.handicapConfig;
        
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    private void CustomUpdate(float deltaTime)
    {
        _handicap = GetHandicap();
    }

    private float GetHandicap()
    {
        var distance = GameService.Instance.Distance;
        for (int i = 0; i < _handicapConfig.handicapConfigs.Count; i++)
        {
            if (_handicapConfig.handicapConfigs[i].fromDistance < distance &&
                distance < _handicapConfig.handicapConfigs[i].toDistance)
            {
                return GetHandicapByGraphicValue(distance, _handicapConfig.handicapConfigs[i]);
            }
        }

        return _handicapConfig.maxHandicapEver;
    }

    private float GetHandicapByGraphicValue(float distance, HandicapConfig.HandicapConfigInfo handicapConfigInfo)
    {
        float factor = (distance - handicapConfigInfo.fromDistance) /
                       (handicapConfigInfo.toDistance - handicapConfigInfo.fromDistance);
        float value = handicapConfigInfo.increaseCurve.Evaluate(factor);
        float handicapRaw = (handicapConfigInfo.maxHandicap - handicapConfigInfo.minHandicap) * value;
        return handicapConfigInfo.minHandicap + handicapRaw;
    }
}
