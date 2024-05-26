using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCounterPuppyController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum WalkCounterPuppyState
    {
        Run,
        Idle
    }

    WalkCounterPuppyState _currentState;

    public WalkCounterPuppyState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case WalkCounterPuppyState.Idle:
                    _animator.CrossFade("Idle", 0.2f);
                    break;
                case WalkCounterPuppyState.Run:
                    _animator.CrossFade("Run", 0.2f);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTimerStart()
    {
        CurrentState = WalkCounterPuppyState.Run;
    }


    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        CurrentState = WalkCounterPuppyState.Idle;
        UI_WalkCounterScene walkCounterScene = (UI_WalkCounterScene)(Managers.UIManager.CurrentSceneUI);
        walkCounterScene.OnStartTimerAction -= OnTimerStart;
        walkCounterScene.OnStartTimerAction += OnTimerStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
