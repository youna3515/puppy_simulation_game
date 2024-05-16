using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform _target;
    Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _target.position;
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.position + _offset;
            transform.position = targetPosition;
        }
    }
}
