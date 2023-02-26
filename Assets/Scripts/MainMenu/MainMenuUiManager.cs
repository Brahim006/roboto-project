using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUiManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void OnStartNewGame()
    {
        gameManager.OnStartNewGame();
    }
}
