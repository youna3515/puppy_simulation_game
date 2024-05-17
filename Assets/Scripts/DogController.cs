using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{

    MoveToTouchedOrClickedPoint _moveToTouchedOrClickedPoint;

    Animator _animator;
    public enum DogState
    {
        Idle,
        Walk,
        Run,
        DoTask,
    }

    [SerializeField]
    DogState _currentState = DogState.Idle;
    public DogState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            switch(_currentState)
            {
                case DogState.Idle:
                    _animator.CrossFade("Idle", 0.5f);
                    break;
                case DogState.Walk:
                    _animator.CrossFade("Walk", 0.5f);
                    break;
                case DogState.Run:
                    _animator.CrossFade("Run", 0.5f);
                    break;
                case DogState.DoTask:
                    break;
                default:
                    break;
            }
        }
    }

    void OnStartMove()
    {
        CurrentState = DogState.Walk;
    }

    void OnEndMove()
    {
        CurrentState = DogState.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _moveToTouchedOrClickedPoint = GetComponent<MoveToTouchedOrClickedPoint>();
        _moveToTouchedOrClickedPoint.MoveStartAction -= OnStartMove;
        _moveToTouchedOrClickedPoint.MoveStartAction += OnStartMove;
        _moveToTouchedOrClickedPoint.MoveEndAction -= OnEndMove;
        _moveToTouchedOrClickedPoint.MoveEndAction += OnEndMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
