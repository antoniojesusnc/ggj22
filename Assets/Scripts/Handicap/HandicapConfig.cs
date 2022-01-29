using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandicapConfig", menuName = "Handicap/new Handicap Config", order = 1)]
public class HandicapConfig : ScriptableObject
{
    [SerializeField]
    public float maxHandicapEver;
    
    public List<HandicapConfigInfo> handicapConfigs;

    [System.Serializable]
    public class HandicapConfigInfo
    {
        public float fromDistance;
        public float toDistance;
        public float minHandicap;
        public float maxHandicap;
        public AnimationCurve increaseCurve;
    }
}
