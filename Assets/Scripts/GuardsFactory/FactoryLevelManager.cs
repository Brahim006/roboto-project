using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryLevelManager : MonoBehaviour
{
    private FactoryLevelManager instance;
    private GameManager gameManager;
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void WinCondition(int health)
    {
        gameManager.TransitionFromFactoryToCity(health);
    }
}
