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
    DogState _currentState = DogState.Idle;

    DogTask _taskAboutToStart;

    
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
                    _animator.CrossFade("Idle", 0.2f);
                    break;
                case DogState.MoveToClickedDest:
                    _animator.CrossFade("Walk", 0.2f);
                    break;
                case DogState.RunToTaskPoint:
                    _animator.CrossFade("Run", 0.2f);
                    break;
                case DogState.Eat:
                    _animator.CrossFade("Eat", 0.2f);
                    break;
                case DogState.Toliet:
                    _animator.CrossFade("Toliet", 0.2f);
                    break;
                case DogState.Sleep:
                    _animator.CrossFade("Sleep", 0.2f);
                    break;
                default:
                    break;
            }
        }
    }

    void OnStartMove()
    {
        CurrentState = DogState.MoveToClickedDest;
    }

    void OnEndMove()
    {
        CurrentState = DogState.Idle;
    }

    void OnStartRunToTaskPoint(DogTask taskType)
    {
        CurrentState = DogState.RunToTaskPoint;
        _taskAboutToStart = taskType;

    }

    void OnEndRunToTaskPoint()
    {
        CurrentState = DogState.Idle;
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

    IEnumerator DoTaskCoroutine(DogTask taskType)
    {
        GameObject taskOnGoingImage = null;
        switch (taskType)
        {
            case DogTask.EatTask:
                CurrentState = DogState.Eat;
                yield return new WaitForSeconds(1.0f);
                _puppyVariable.Eat();
                break;
            case DogTask.GoToiletTask:
                CurrentState = DogState.Toliet;
                yield return new WaitForSeconds(2.0f);
                break;
            case DogTask.SleepTask:
                CurrentState = DogState.Sleep;
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "자는 중...";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Sleep();
                break;
            case DogTask.TakeWalkTask:
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "산책 시작!";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                break;
            case DogTask.TakeWashTask:
                taskOnGoingImage = Instantiate<GameObject>(_taskOnGoingPrefab);
                taskOnGoingImage.GetComponentInChildren<Text>().text = "씻는 중...";
                StartCoroutine(ShowTaskOnGoing(taskOnGoingImage.GetComponentInChildren<Image>(), 1.0f, 2.0f));
                yield return new WaitForSeconds(4.1f);
                _puppyVariable.Wash();
                break;
            default:
                break;
        }
        CurrentState = DogState.Idle;
        if (taskOnGoingImage != null) GameObject.Destroy(taskOnGoingImage);
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case DogState.Idle:
                break;
            case DogState.MoveToClickedDest:
                break;
            case DogState.RunToTaskPoint:
                break;
        }
    }
}
