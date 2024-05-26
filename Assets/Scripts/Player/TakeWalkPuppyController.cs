using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TakeWalkPuppyController : MonoBehaviour
{
    public enum TakeWalkPuppyState
    {
        Idle,
        Runnig,
        Dead
    }

    TakeWalkPuppyState _currentState;
    public TakeWalkPuppyState CurrentState
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
                case TakeWalkPuppyState.Idle:
                    _animator.CrossFade("Idle", 0.2f);
                    break;
                case TakeWalkPuppyState.Runnig:
                    _animator.CrossFade("Running", 0.2f);
                    break;
                case TakeWalkPuppyState.Dead:
                    _animator.CrossFade("Dead", 0.2f);
                    break;
            }
        }
    }

    public Action OnGameOverAction = null;

    VerticalHalfScreenRecognizer _verticalHalfScreenRecognizer;

    public float _moveSpeed = 10f; // 이동 속도

    private Animator _animator;

    private float _roadWidth; // RoadManager에서 설정된 길의 폭


    private Coroutine currentMoveCoroutine; // 현재 이동 코루틴을 추적

    public void SetRoadWidth(float width)
    {
        _roadWidth = width + 4;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        CurrentState = TakeWalkPuppyState.Idle;

        _verticalHalfScreenRecognizer = GetComponent<VerticalHalfScreenRecognizer>();

        _verticalHalfScreenRecognizer.LeftHalfAction -= MoveLeft;
        _verticalHalfScreenRecognizer.LeftHalfAction += MoveLeft;

        _verticalHalfScreenRecognizer.RightHalfAction -= MoveRight;
        _verticalHalfScreenRecognizer.RightHalfAction += MoveRight;

        StartCoroutine(Countdown());

        currentMoveCoroutine = null;
    }
    IEnumerator Countdown()
    {
        int interval = Managers.DataManager.TakeWalkStartInterval;
        yield return new WaitForSeconds(interval + 1);
        CurrentState = TakeWalkPuppyState.Runnig;
    }

    void Update()
    {
        if (CurrentState == TakeWalkPuppyState.Runnig)
        {
            MoveForward();
            ClampPosition();
        }
        _moveSpeed += Time.deltaTime;
    }

    void MoveLeft()
    {
        // 현재 진행 중인 이동 코루틴이 있으면 중지
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        // 새로운 이동 코루틴 시작
        currentMoveCoroutine = StartCoroutine(MoveInDirection(Vector3.left));
    }

    void MoveRight()
    {
        // 현재 진행 중인 이동 코루틴이 있으면 중지
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        // 새로운 이동 코루틴 시작
        currentMoveCoroutine = StartCoroutine(MoveInDirection(Vector3.right));
    }

    IEnumerator MoveInDirection(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.3f)
        {
            // 목표 방향으로 부드럽게 이동
            Vector3 newPosition = transform.position + direction * _moveSpeed * Time.deltaTime * 0.5f;
            float halfRoadWidth = _roadWidth / 2 - 3.0f;
            newPosition.x = Mathf.Clamp(newPosition.x, -halfRoadWidth, halfRoadWidth);

            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;
        float halfRoadWidth = _roadWidth / 2;
        position.x = Mathf.Clamp(position.x, -halfRoadWidth, halfRoadWidth);
        transform.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Collided with obstacle");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        
        CurrentState = TakeWalkPuppyState.Dead;
        yield return new WaitForSeconds(1); // 2초 대기

        OnGameOverAction.Invoke();
    }

}
