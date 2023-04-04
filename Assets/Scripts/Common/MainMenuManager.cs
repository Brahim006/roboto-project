using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menuContent;
    
    public void ToggleCredits()
    {
        if(menuContent.active)
        {
            credits.SetActive(true);
            menuContent.SetActive(false);
        }
        else
        {
            credits.SetActive(false);
            menuContent.SetActive(true);
        }
    }
}
