using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Defines;

public class PuppyController : MonoBehaviour
{

    MoveToTouchedOrClickedPoint _moveToTouchedOrClickedPoint;
    PuppyTask _puppyTask;
    PuppyVariable _puppyVariable;

    Animator _animator;

    [SerializeField]
    PuppyState _currentState = PuppyState.Idle;

    PuppyTaskType _taskAboutToStart;

    public PuppyState CurrentState
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
                case PuppyState.Idle:
                    _animator.CrossFade("Idle", 0.2f);
                    break;
                case PuppyState.MoveToClickedDest:
                    _animator.CrossFade("Walk", 0.2f);
                    break;
                case PuppyState.RunToTaskPoint:
                    _animator.CrossFade("Run", 0.2f);
                    break;
                case PuppyState.Eat:
                    _animator.CrossFade("Eat", 0.2f);
                    break;
                case PuppyState.Toliet:
                    _animator.CrossFade("Toliet", 0.2f);
                    break;
                case PuppyState.Sleep:
                    _animator.CrossFade("Sleep", 0.2f);
                    break;
                case PuppyState.WantToilet:
                    _animator.CrossFade("WantToilet", 0.2f);
                    break;
                default:
                    break;
            }
        }
    }

    void OnStartMove()
    {
        _puppyTask.IsRunningToTaskPoint = false;
        CurrentState = PuppyState.MoveToClickedDest;
    }

    void OnEndMove()
    {
        CurrentState = PuppyState.Idle;
    }

    void OnStartRunToTaskPoint(PuppyTaskType taskType)
    {
        _moveToTouchedOrClickedPoint.IsMoving = false;
        CurrentState = PuppyState.RunToTaskPoint;
        _taskAboutToStart = taskType;
    }

    void OnEndRunToTaskPoint()
    {
        CurrentState = PuppyState.Idle;
        StartCoroutine(DoTaskCoroutine(_taskAboutToStart));
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _puppyTask = GetComponent<PuppyTask>();
        _moveToTouchedOrClickedPoint = GetComponent<MoveToTouchedOrClickedPoint>();
        _puppyVariable = GetComponent<PuppyVariable>();

        _puppyTask.StartRunToTaskPointAction -= OnStartRunToTaskPoint;
        _puppyTask.StartRunToTaskPointAction += OnStartRunToTaskPoint;
        _puppyTask.EndRunToTaskPointAction -= OnEndRunToTaskPoint;
        _puppyTask.EndRunToTaskPointAction += OnEndRunToTaskPoint;

        _moveToTouchedOrClickedPoint.MoveStartAction -= OnStartMove;
        _moveToTouchedOrClickedPoint.MoveStartAction += OnStartMove;
        _moveToTouchedOrClickedPoint.MoveEndAction -= OnEndMove;
        _moveToTouchedOrClickedPoint.MoveEndAction += OnEndMove;

        CurrentState = PuppyState.Idle;
    }
    

    IEnumerator DoTaskCoroutine(PuppyTaskType taskType)
    {
        GameObject taskOnGoingImage = null;
        switch (taskType)
        {
            case PuppyTaskType.EatTask:
                CurrentState = PuppyState.Eat;
                yield return new WaitForSeconds(1.0f);
                _puppyVariable.Eat();
                break;
            case PuppyTaskType.GoToiletTask:
                CurrentState = PuppyState.Toliet;
                yield return new WaitForSeconds(2.0f);
                _puppyVariable.GoToilet();
                break;
            case PuppyTaskType.SleepTask:
                CurrentState = PuppyState.Sleep;
                Managers.UIManager.ShowPopupUI<UI_TaskOnGoing>().TaskType = PuppyTaskType.SleepTask;
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Sleep();
                break;
            case PuppyTaskType.TakeWalkTask:
                Managers.UIManager.ShowPopupUI<UI_TaskOnGoing>().TaskType = PuppyTaskType.TakeWalkTask;
                yield return new WaitForSeconds(2.1f);
                _puppyVariable.TakeWalk();
                break;
            case PuppyTaskType.TakeWashTask:
                Managers.UIManager.ShowPopupUI<UI_TaskOnGoing>().TaskType = PuppyTaskType.TakeWashTask;
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Wash();
                break;
            case PuppyTaskType.GoWrongToiletTask:
                CurrentState = PuppyState.Toliet;
                yield return new WaitForSeconds(2.0f);
                _puppyVariable.GoWrongToilet();
                break;
            default:
                break;
        }
        CurrentState = PuppyState.Idle;
        if (taskOnGoingImage != null) GameObject.Destroy(taskOnGoingImage);
    }


    void OnIdle()
    {
        if (_puppyVariable.Toilet >= 50.0f)
        {
            CurrentState = PuppyState.WantToilet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case PuppyState.Idle:
                OnIdle();
                break;
            case PuppyState.MoveToClickedDest:
                break;
            case PuppyState.RunToTaskPoint:
                break;
        }
    }
}
