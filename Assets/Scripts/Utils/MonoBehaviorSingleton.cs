using UnityEngine;

public class MonoBehaviorSingleton<T> : MonoBehaviour
{
    public static T Instance;
    
    public T _instance;
    
    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        _instance = this.GetComponent<T>();
    }
}
