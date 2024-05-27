using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCounter : MonoBehaviour
{
    float _threshold = 0.3f; // ���ӵ��� ��ȭ�� �������� �νĵ� �ּ� ��
    int _stepCount = 0; // ���� �� ����

    public int StepCount { get { return _stepCount; } }

    Vector3 _previousAcceleration;
    bool _isStep;

    void Start()
    {
        _previousAcceleration = Input.acceleration;
        _isStep = false;
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        Vector3 deltaAcceleration = acceleration - _previousAcceleration;

        if (!_isStep && deltaAcceleration.sqrMagnitude >= _threshold)
        {
            _isStep = true;
            _stepCount++;
        }

        if (_isStep && deltaAcceleration.sqrMagnitude < _threshold)
        {
            _isStep = false;
        }

        _previousAcceleration = acceleration;
    }
}
