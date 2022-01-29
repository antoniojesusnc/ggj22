using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Object = UnityEngine.Object;

public class MonoBehaviorSingleton<T> : MonoBehaviour where T : Object
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
            }
            return _instance;
        }
    }

    private static T _instance;

    protected virtual void Awake()
    {
        if (Instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
        }
    }

    public virtual void Dispose()
    {
        
    }
}
