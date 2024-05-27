using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCounter : MonoBehaviour
{
    float _threshold = 0.3f; // 가속도의 변화가 걸음으로 인식될 최소 값
    int _stepCount = 0; // 걸음 수 저장

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
