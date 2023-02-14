using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    private GameObject secondFloor;
    private GameObject thirdFloor;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            secondFloor = GameObject.FindWithTag("second-floor");
            Debug.Log(secondFloor);
            secondFloor.SetActive(false);
            thirdFloor = GameObject.FindWithTag("third-floor");
            thirdFloor.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static LevelController GetInstance()
    {
        return instance;
    }

    public void SwitchSecondFloor()
    {
        secondFloor.SetActive(!secondFloor.active);
    }

    public void SwitchThirdFloor()
    {
        thirdFloor.SetActive(!thirdFloor.active);
    }
}
