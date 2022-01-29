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

    protected override void SetAsAvailable()
    {
        gameObject.SetActive(true);
    }

    protected override void SetAsUnAvailable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != _layerTohit)
        {
            return;
        }
        
        OnHitObstacule?.Invoke();
    }
}
