using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;

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
        CharacterController.OnDeath += OnGameQuit;
    }

    public void OnGameQuit()
    {
        Debug.Log($"OnDeath recibido por {gameObject.name}");
        // TODO: Cambiar esto al buildear
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
