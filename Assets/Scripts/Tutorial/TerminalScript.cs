using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] Transform piecesInstanciatorMembers;
    [SerializeField] Transform piecesInstanciatorCore;
    private float _timeFromLastInstanciation = 0;

    [SerializeField] GameObject[] memberPartsPrebabs;
    [SerializeField] GameObject[] corePartsPrebabs;

    private List<GameObject> membersPartsInstances = new List<GameObject>();
    private List<GameObject> corePartsInstances = new List<GameObject>();

    private Dictionary<string, int> partTypesAmount = new Dictionary<string, int>();

    private static readonly int INSTANCIATION_TIMES_MAX = 3;

    private void Start()
    {
        FillRobotPartsLists();
        partTypesAmount.Add("members", memberPartsPrebabs.Length);
        partTypesAmount.Add("core", corePartsPrebabs.Length);
    }

    private void Update()
    {
        _timeFromLastInstanciation += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_timeFromLastInstanciation > 1 && Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            InstanciateParts();
            _timeFromLastInstanciation = 0;
        }
    }

    private void FillRobotPartsLists()
    {
        for(int i = 0; i < INSTANCIATION_TIMES_MAX; i++)
        {
            membersPartsInstances.Add(memberPartsPrebabs[Random.Range(0, memberPartsPrebabs.Length)]);
            corePartsInstances.Add(corePartsPrebabs[Random.Range(0, corePartsPrebabs.Length)]);
        }
    }

    private void InstanciateParts()
    {
        if(membersPartsInstances.Count != 0 && corePartsInstances.Count != 0)
        {
            Instantiate(membersPartsInstances[membersPartsInstances.Count - 1], piecesInstanciatorMembers.position, piecesInstanciatorMembers.rotation);
            membersPartsInstances.RemoveAt(membersPartsInstances.Count - 1);
            Instantiate(corePartsInstances[corePartsInstances.Count - 1], piecesInstanciatorCore.position, piecesInstanciatorCore.rotation);
            corePartsInstances.RemoveAt(corePartsInstances.Count - 1);
        }
    }
}
