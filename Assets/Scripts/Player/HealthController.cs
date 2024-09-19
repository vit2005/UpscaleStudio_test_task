using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float _currentHealth;

    public Action<float> OnHealthChanged; // in percentage

    public void Init()
    {
        _currentHealth = maxHealth;
        OnHealthChanged?.Invoke(1f);
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
            _currentHealth = 0;

        OnHealthChanged?.Invoke(_currentHealth / maxHealth);
    }
}
