using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWithHealth : MonoBehaviour
{
    protected int INITIAL_HEALTH = 100;
    protected int health;

    protected virtual void Start()
    {
        health = INITIAL_HEALTH;
    }

    
    public void OnReceiveDamage(int amount)
    {
        var newHealth = health;
        newHealth -= amount;
        if (newHealth < 0) newHealth = 0;
        health = newHealth;
    }
}
