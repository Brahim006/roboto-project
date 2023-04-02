using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    private GameManager instance;
    private PlataformerPlayer player;

    public int transitioningHealth = 0;
    public int cityLevelState = 0;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlataformerPlayer>();
        player.OnDeath += OnGameQuit;
    }

    private void OnDestroy()
    {
        player.OnDeath -= OnGameQuit;
    }

    public void TransitionFromTutorialToCity()
    {
        SceneManager.LoadScene(1);
    }

    public void TransitionFromCityToFactory()
    {
        SceneManager.LoadScene(2);
    }

    public void TransitionFromFactoryToCity(int currentHealth)
    {
        transitioningHealth = currentHealth;
        cityLevelState = 1;
        SceneManager.LoadScene(1);
    }
    public void OnGameQuit()
    {
        // TODO: Cambiar esto al buildear
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
