using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    private float timeToReset = 0;
    [SerializeField] Transform subsPartsInstanciator;
    [SerializeField] Transform principalsPartsInstanciator;

    [SerializeField] private GameObject[] subsPartsPrefabs;
    [SerializeField] private GameObject[] principalsPartsPrefabs;

    private List<GameObject> subsPartsList = new List<GameObject>();
    private List<GameObject> principalsPartsList = new List<GameObject>();

    private Dictionary<string,int> partsCount= new Dictionary<string,int>();

    private void Start()
    {
        FillRobotPartsList();
        partsCount.Add("subsParts", subsPartsPrefabs.Length);
        partsCount.Add("principalParts",principalsPartsPrefabs.Length);
    }
    private void Update()
    {
        timeToReset += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(timeToReset >= 1 && Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            RobotPartsInstantiate();
            timeToReset= 0;
        }
    }
    private void FillRobotPartsList()
    {
        for(int i=0; i<3; i++)
        {
            subsPartsList.Add(subsPartsPrefabs[Random.Range(0,subsPartsPrefabs.Length)]);
            principalsPartsList.Add(principalsPartsPrefabs[Random.Range(0, principalsPartsPrefabs.Length)]);
        }
    }
    private void RobotPartsInstantiate()
    {
        if (subsPartsList.Count != 0 && principalsPartsList.Count != 0)
        {
            var subsParts = subsPartsList[subsPartsList.Count - 1];
            var principalParts = principalsPartsList[principalsPartsList.Count - 1];
            Instantiate(subsParts, subsPartsInstanciator.position, subsPartsInstanciator.rotation);
            Instantiate(principalParts, principalsPartsInstanciator.position, principalsPartsInstanciator.rotation);
            subsPartsList.RemoveAt(subsPartsList.Count - 1);
            principalsPartsList.RemoveAt(principalsPartsList.Count - 1);
         }
    }
}
