using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotWithHealt : MonoBehaviour
{
    private static readonly int MAX_HEALTH = 100;
    private static readonly int MIN_HEALTH = 0;

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
        }
        else
        {
            health = newHealthValue;
        }
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
