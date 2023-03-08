using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotWithHealt : MonoBehaviour
{
    private static readonly int MAX_HEALTH = 100;
    private static readonly int MIN_HEALTH = 0;

    public static event Action<int> OnHealthChange;
    public static event Action OnDeath;

    private int health;
    protected virtual void Start()
    {
        health = MAX_HEALTH;
    }

    public void InitHealth(int initialHealth)
    {
        SetHealth(initialHealth);
    }

    private void SetHealth(int newHealthValue)
    {
        if(newHealthValue > MAX_HEALTH) 
        { 
            health = MAX_HEALTH;
        }
        else if(newHealthValue < MIN_HEALTH)
        {
            health = MIN_HEALTH;
            OnDeath?.Invoke();
            Debug.Log($"OnDeath llamado por {gameObject.name}");
        }
        else
        {
            health = newHealthValue;
        }
        OnHealthChange?.Invoke(health);
        Debug.Log($"OnHealthChange llamado por {gameObject.name}");
    }

    public void OnHeal(int healAmount)
    {
        SetHealth(health + healAmount);
    }

    public void OnReceiveDamage(int damage)
    {
        SetHealth(health - damage);
    }
}
