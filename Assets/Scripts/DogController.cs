using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    Animator _animator;
    public enum DogState
    {
        Idle,
        Walk,
        Run,
        DoTask,
    }

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
                    _animator.Play("Idle");
                    break;
                case DogState.Walk:
                    _animator.Play("Walk");
                    break;
                case DogState.Run:
                    _animator.Play("Run");
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
