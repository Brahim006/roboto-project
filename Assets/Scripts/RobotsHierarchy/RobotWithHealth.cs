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

    protected void SetHealth(int newHealthValue)
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

    protected void OnHeal(int healAmount)
    {
        SetHealth(health + healAmount);
    }

    protected void OnReceiveDamage(int damage)
    {
        SetHealth(health - damage);
    }
}
