using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PauseMenu instance;
    private GameManager gameManager;
    private GameObject canvasContainer;

    private float _initialTimeScale;
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnToggleMenu();
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        canvasContainer = transform.Find("CanvasContainer").gameObject;
        _initialTimeScale = Time.timeScale;
    }
    public void OnToggleMenu()
    {
        bool isActive = canvasContainer.active;
        if(isActive)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        canvasContainer.SetActive(!isActive);
    }

    public void OnReturnToMainMenu()
    {
        Time.timeScale = 1;
        Destroy(gameManager.gameObject);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void OnGameQuit()
    {
        gameManager.OnGameQuit();
    }
}
