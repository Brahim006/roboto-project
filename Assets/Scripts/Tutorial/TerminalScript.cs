using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] private GameObject memberPartsInstantiator;
    [SerializeField] private GameObject corePartsInstantiator;
    [SerializeField] private GameObject[] memberPartsPrefabs;
    [SerializeField] private GameObject[] corePartsPrefabs;
    private Queue<GameObject> memberPartsQueue = new Queue<GameObject>();
    private Queue<GameObject> corePartsQueue = new Queue<GameObject>();

    private static readonly int PARTS_PER_QUEUE = 3;

    private void Start()
    {
        FillRobotPartsQueues();
    }

    private void OnTriggerStay(Collider other)
    {
        var areQueuesEmpty = memberPartsQueue.Count == 0 || corePartsQueue.Count == 0;
        if(
            Input.GetKeyDown(KeyCode.E) &&
            !areQueuesEmpty &&
            other.TryGetComponent<CharacterController>(out CharacterController player)
          )
        {
            player.PressButton(transform.position);
            InstantiateRobotParts();
        }
    }

    private void InstantiateRobotParts()
    {
        Instantiate(memberPartsQueue.Dequeue(), memberPartsInstantiator.transform.position, memberPartsInstantiator.transform.rotation);
        Instantiate(corePartsQueue.Dequeue(), corePartsInstantiator.transform.position, corePartsInstantiator.transform.rotation);
    }

    private void FillRobotPartsQueues()
    {
        for(int i = 0; i < PARTS_PER_QUEUE; i++)
        {
            memberPartsQueue.Enqueue(memberPartsPrefabs[Random.Range(0, memberPartsPrefabs.Length)]);
            corePartsQueue.Enqueue(corePartsPrefabs[Random.Range(0, corePartsPrefabs.Length)]);
        }
    }
}
