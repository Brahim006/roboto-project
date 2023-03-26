using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;
    private PlataformerPlayer player;
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

    public void OnGameQuit()
    {
        // TODO: Cambiar esto al buildear
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
