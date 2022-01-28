using UnityEngine;

public class MonoBehaviorSingleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        Instance = this.GetComponent<T>();
    }
}
