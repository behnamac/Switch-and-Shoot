using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private float _currentHealth;
    private Rigidbody _rigid;
    public bool ActiveDead { get; set; }

    private void Awake()
    {
        _currentHealth = maxHealth;

        TryGetComponent(out _rigid);
    }
    
    public void TakeDamage(float value)
    {
        _currentHealth -= value;

        if (_currentHealth <= 0)
            Dead();
    }
    private void Dead()
    {
        ActiveDead = true;
        
        GameManager.Instance.LevelFail();
    }
}
