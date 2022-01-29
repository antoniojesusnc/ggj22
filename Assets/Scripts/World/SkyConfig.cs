using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkyConfig", menuName = "World/new Sky Config", order = 1)]
public class SkyConfig : ScriptableObject
{
    [Header("SkyConfig")]
    public List<SkyConfigInfo> skyes;

    [System.Serializable]
    public class SkyConfigInfo
    {
        public float distanceInit;
        public Color color;
    }
}
