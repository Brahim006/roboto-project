using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLevelManager : MonoBehaviour
{
    private CityLevelManager instance;

    [SerializeField] private GameObject plataformerState;
    [SerializeField] private GameObject combatantState;
    [SerializeField] private GameObject sun;

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
        if(gameManager.cityLevelState == 0)
        {
            plataformerState.SetActive(true);
            sun.transform.Rotate(45, 0, 0);
        }
        else
        {
            combatantState.SetActive(true);
        }
    }

    public void EnterFactory()
    {
        gameManager.TransitionFromCityToFactory();
    }
}
