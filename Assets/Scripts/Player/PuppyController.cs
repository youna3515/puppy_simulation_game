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
    [SerializeField]
    GameObject _taskOnGoingPrefab;

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
                default:
                    break;
            }
        }
    }

    void OnStartMove()
    {
        CurrentState = PuppyState.MoveToClickedDest;
    }

    void OnEndMove()
    {
        CurrentState = PuppyState.Idle;
    }

    void OnStartRunToTaskPoint(PuppyTaskType taskType)
    {
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
    }
    IEnumerator ShowTaskOnGoing(Image image, float fadeTime, float waitTime)
    {
        image.fillAmount = 0;
        float time = 0;
        while (time < fadeTime)
        {
            image.fillAmount += Time.deltaTime / fadeTime;
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        while (time > 0)
        {
            image.fillAmount -= Time.deltaTime / fadeTime;
            time -= Time.deltaTime;
            yield return null;
        }
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
                break;
            case PuppyTaskType.SleepTask:
                CurrentState = PuppyState.Sleep;
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "Go to sleep";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Sleep();
                break;
            case PuppyTaskType.TakeWalkTask:
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "Take a walk";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                break;
            case PuppyTaskType.TakeWashTask:
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "Take a bath";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Wash();
                break;
            default:
                break;
        }
        CurrentState = PuppyState.Idle;
        if (taskOnGoingImage != null) GameObject.Destroy(taskOnGoingImage);
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case PuppyState.Idle:
                break;
            case PuppyState.MoveToClickedDest:
                break;
            case PuppyState.RunToTaskPoint:
                break;
        }
    }
}
