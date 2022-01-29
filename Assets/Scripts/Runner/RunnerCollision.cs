using System;
using UnityEngine;
using UnityEngine.Events;

public class RunnerCollision : RunnerSwitcher
{
    [SerializeField]
    private LayerMask _layerTohit;
    
    [SerializeField] 
    private UnityEvent OnHitObstacule;
    
    [SerializeField] 
    private Rigidbody2D _rigidbody;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((1 << collider.gameObject.layer) != _layerTohit)
        {
            return;
        }
        
        OnHitObstacule?.Invoke();
    }
}
