using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Defines;

public class PuppyTask : MonoBehaviour
{
    NavMeshAgent _agent;
    GameObject _taskPoint;

    public Action<PuppyTaskType> StartRunToTaskPointAction = null;
    public Action EndRunToTaskPointAction = null;

    bool _bIsRunningToTaskPoint = false;
    public bool IsRunningToTaskPoint
    {
        get
        {
            return _bIsRunningToTaskPoint;
        }
        set
        {
            _bIsRunningToTaskPoint = value;
        }
    }

    float _lookRotationSpeed = 8f;

    float _radius = 8.0f;

    public GameObject TaskPoint
    {
        get { return _taskPoint; }
        set { _taskPoint = value; }
    }

    [SerializeField]
    Dictionary<PuppyTaskType, Vector3> _taskPointDic;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _taskPointDic = new Dictionary<PuppyTaskType, Vector3>();

        for (int i = 0; i < TaskPoint.transform.childCount; i++)
        {
            _taskPointDic.Add((PuppyTaskType)i, TaskPoint.transform.GetChild(i).position);
        }
        
    }

    private void Update()
    {
        if (_bIsRunningToTaskPoint == true)
        {
            FaceTarget();
            if ((transform.position - _agent.destination).magnitude <= _agent.stoppingDistance)
            {
                EndRunToTaskPointAction.Invoke();
                _bIsRunningToTaskPoint = false;
            }
        }
    }

    public void DoTask(PuppyTaskType taskType)
    {
        if (StartRunToTaskPointAction != null)
            StartRunToTaskPointAction.Invoke(taskType);


        Vector3 dest = Vector3.zero;
        if (taskType == PuppyTaskType.GoWrongToiletTask)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _radius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _radius, NavMesh.AllAreas))
            {
                dest = hit.position;
            }
        }
        else
        {
            _taskPointDic.TryGetValue(taskType, out dest);
        }
        
        _agent.destination = dest;

        _bIsRunningToTaskPoint = true;
    }

    void FaceTarget()
    {
        if (_agent == null) return;

        if (_agent.pathPending || _agent.remainingDistance > _agent.stoppingDistance)
        {
            Vector3 direction = (_agent.steeringTarget - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
            }
        }
    }
}
