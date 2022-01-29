using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandicapController : MonoBehaviour
{
    [SerializeField] public float _handicap;
    public float Handicap => _handicap;

    [SerializeField] private HandicapConfig _handicapConfig;


    public void OnHit()
    {
        TrackManager.Instance.CleanAfterHit();
    }
}
