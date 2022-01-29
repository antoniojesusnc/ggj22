using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundConfig", menuName = "World/new Background Config", order = 1)]
public class BackgroundConfig : ScriptableObject
{
    [Header("Background")]
    public List<BackgroundConfigInfo> backgrounds;

    [System.Serializable]
    public class BackgroundConfigInfo
    {
        public float distanceInit;
        public Sprite sprite;
        public float size;
        public float speedPercentageOfSpeed;
    }
}
