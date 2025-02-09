using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTouchedOrClickedPoint : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] LayerMask _clickableLayers;


    float _lookRotationSpeed = 8f;
    bool _bIsMoving = false;
    public bool IsMoving
    {
        get
        {
            return _bIsMoving;
        }
        set
        {
            _bIsMoving = value;
        }
    }

    public Action MoveStartAction = null;
    public Action MoveEndAction = null;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        Managers.InputManager.PointerDownInputAction -= ClickOrTouchToMove;
        Managers.InputManager.PointerDownInputAction += ClickOrTouchToMove;
    }

    void Update()
    {
        if (_bIsMoving)
        {
            FaceTarget();
            CheckIfMovementCompleted();
        }

    }

    void ClickOrTouchToMove(Vector3 inputPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(inputPosition), out hit, 100, _clickableLayers))
        {
            _agent.destination = hit.point;
            _bIsMoving = true;
            MoveStartAction.Invoke();
        }
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

    void CheckIfMovementCompleted()
    {
        if ((transform.position - _agent.destination).magnitude <= _agent.stoppingDistance)
        {
            MoveEndAction.Invoke();
            _bIsMoving = false;
        }
    }
}
