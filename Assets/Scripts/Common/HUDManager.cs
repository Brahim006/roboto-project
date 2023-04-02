using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private HUDManager instance;

    [SerializeField] private Slider healthBar;

    private static readonly int MIN_HEALTH_VALUE = 0;
    private static readonly int MAX_HEALTH_VALUE = 100;
    private void Awake()
    {
        if(instance is null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        healthBar.minValue = MIN_HEALTH_VALUE;
        healthBar.maxValue = MAX_HEALTH_VALUE;
        healthBar.value = MAX_HEALTH_VALUE;
    }

    public void AssignPlayer(CombatantPlayer player)
    {
        SetHealth(player.GetHealth());
        player.OnHealtChange += SetHealth;
    }
    public void SetHealth(int health)
    {
        if(health < MIN_HEALTH_VALUE)
        {
            healthBar.value = MIN_HEALTH_VALUE;
        }
        else if(health > MAX_HEALTH_VALUE)
        {
            healthBar.value = MAX_HEALTH_VALUE;
        }
        else
        {
            healthBar.value = health;
        }
    }
}
