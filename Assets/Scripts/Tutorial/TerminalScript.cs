using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject memberPartsInstantiator;
    [SerializeField] private GameObject corePartsInstantiator;
    [SerializeField] private RobotPartsColetion memberPartsPrefabs;
    [SerializeField] private RobotPartsColetion corePartsPrefabs;
    [SerializeField] private TutorialWorker[] workers;
    private Queue<GameObject> memberPartsQueue = new Queue<GameObject>();
    private Queue<GameObject> corePartsQueue = new Queue<GameObject>();

    private static readonly int PARTS_PER_QUEUE = 3;
    private static readonly float SECONDS_BEFORE_RANT = 5f;

    private float _rantOffset;
    private void Start()
    {
        _rantOffset = SECONDS_BEFORE_RANT;
        FillRobotPartsQueues();
    }

    private void Update()
    {
        if(memberPartsQueue.Count != 0)
        {
            if(_rantOffset < 0)
            {
                OnWorkersRant();
            } else
            {
                _rantOffset -= Time.deltaTime;
            }
        }
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
            OnWorkersStopRanting();
        }
    }

    private void InstantiateRobotParts()
    {
        Instantiate(memberPartsQueue.Dequeue(), memberPartsInstantiator.transform.position, memberPartsInstantiator.transform.rotation);
        Instantiate(corePartsQueue.Dequeue(), corePartsInstantiator.transform.position, corePartsInstantiator.transform.rotation);
        if(memberPartsQueue.Count == 0 || corePartsQueue.Count == 0)
        {
            levelManager.SetLevelFirstMilestone();
        }
    }

    private void FillRobotPartsQueues()
    {
        for(int i = 0; i < PARTS_PER_QUEUE; i++)
        {
            memberPartsQueue.Enqueue(memberPartsPrefabs.parts[Random.Range(0, memberPartsPrefabs.parts.Length)]);
            corePartsQueue.Enqueue(corePartsPrefabs.parts[Random.Range(0, corePartsPrefabs.parts.Length)]);
        }
    }

    private void OnWorkersRant()
    {
        foreach(TutorialWorker worker in workers)
        {
            worker.Rant();
        }
    }

    private void OnWorkersStopRanting()
    {
        foreach (TutorialWorker worker in workers)
        {
            worker.StopRanting();
        }
    }
}
