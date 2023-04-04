using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLevelManager : MonoBehaviour
{
    private CityLevelManager instance;

    [SerializeField] private GameObject plataformerState;
    [SerializeField] private GameObject combatantState;
    [SerializeField] private GameObject sun;
    [SerializeField] private CombatantPlayer combatantPlayer;

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
        sun.transform.Rotate(145, -100, 0);
        if(gameManager.cityLevelState == 0)
        {
            plataformerState.SetActive(true);
            combatantState.SetActive(false);
        }
        else
        {
            combatantState.SetActive(true);
            plataformerState.SetActive(false);
            if(combatantPlayer.gameObject.active)
            {
                gameManager.SetLastCheckpoint(combatantPlayer.transform.position);
                combatantPlayer.OnReceiveDamage(100 - gameManager.transitioningHealth);
            }
        }
    }

    public void EnterFactory()
    {
        gameManager.TransitionFromCityToFactory();
    }
}
