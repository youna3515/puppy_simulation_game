using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    // public static PlayerController Instance;

    VerticalHalfScreenRecognizer _verticalHalfScreenRecognizer;

    public float _moveSpeed = 10f; // 이동 속도
    // public float _animationSpeed = 7f; // 애니메이션 속도
    //public Camera mainCamera;

    //private Vector3 _targetPosition; // 목표 위치
    //private bool _bIsRunning = false;
    private Animator _animator;
    //public BackgroundScroller _backgroundScroller; // 배경 스크롤러 참조

    //private float screenLeftEdge;
    //private float screenRightEdge;
    private float _roadWidth; // RoadManager에서 설정된 길의 폭

    //private float distanceTravelled = 0f;
    //private Vector3 lastPosition;

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
        //StartCoroutine(StartCountdown());

        // 화면 경계 계산
        //float cameraZDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        //screenLeftEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraZDistance)).x;
        //screenRightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, cameraZDistance)).x;

        //lastPosition = transform.position;
    }
    IEnumerator Countdown()
    {
        int interval = Managers.DataManager.TakeWalkStartInterval;
        yield return new WaitForSeconds(interval + 1);
        CurrentState = TakeWalkPuppyState.Runnig;
    }

    /*
    IEnumerator StartCountdown()
    {
        // 카운트다운 텍스트를 화면에 표시
        CountdownTextController countdownText = FindObjectOfType<CountdownTextController>();
        if (countdownText != null)
        {
            countdownText.StartCountdown();
        }

        // 게임 시작 전 카메라 위치 초기화
        //UpdateCameraPosition();

        yield return new WaitForSeconds(3); // 3초 카운트다운
        _bIsRunning = true;
        _animator.SetFloat("Speed", _animationSpeed); // 카운트다운 후 애니메이션 속도 설정하여 달리기 시작

        // 배경 스크롤러 설정
        if (_backgroundScroller != null)
        {
            _backgroundScroller.scrollSpeed = _moveSpeed;
        }
    }
    */
    void Update()
    {
        if (CurrentState == TakeWalkPuppyState.Runnig)
        {
            //HandleTouchInput();
            MoveForward();
            ClampPosition();
            //UpdateAnimator();
            //UpdateCameraPosition();
            //UpdateScore();
        }
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
    /*
    void HandleTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inputPosition = Input.mousePosition;
            Vector3 direction;

            if (inputPosition.x < Screen.width / 2)
            {
                // 화면의 왼쪽 클릭
                direction = Vector3.left;
            }
            else
            {
                // 화면의 오른쪽 클릭
                direction = Vector3.right;
            }

            // 현재 진행 중인 이동 코루틴이 있으면 중지
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }

            // 새로운 이동 코루틴 시작
            currentMoveCoroutine = StartCoroutine(MoveInDirection(direction));
        }
    }
    */
    IEnumerator MoveInDirection(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.3f)
        {
            // 목표 방향으로 부드럽게 이동
            Vector3 newPosition = transform.position + direction * _moveSpeed * Time.deltaTime;
            float halfRoadWidth = _roadWidth / 2;
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

    /*
    void UpdateAnimator()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", _animationSpeed); // 항상 _animationSpeed 값을 사용하여 Speed 파라미터 설정
        }
    }
    */

    /*
    void UpdateCameraPosition()
    {
        if (mainCamera != null)
        {
            Vector3 newCameraPosition = mainCamera.transform.position;
            newCameraPosition.x = 0;
            newCameraPosition.y = transform.position.y + 20; // 카메라를 위로 더 올려서 강아지를 내려다보게 설정
            newCameraPosition.z = transform.position.z - 25; // 카메라와 플레이어의 Z 거리 유지
            mainCamera.transform.position = newCameraPosition;
        }
    }
    */

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
        // 강아지의 애니메이터에서 Die 트리거 설정
        //_animator.SetTrigger("Die");

        // 게임 종료 로직 (예: 플레이어의 움직임 멈추기 등)
        //_bIsRunning = false;

        CurrentState = TakeWalkPuppyState.Dead;
        //_moveSpeed = 0;
        //_animationSpeed = 0;

        yield return new WaitForSeconds(1); // 2초 대기

        OnGameOverAction.Invoke();
        // TakeWalkManager 인스턴스가 null인지 확인
        /*
        if (TakeWalkManager.Instance != null)
        {
            TakeWalkManager.Instance.EndGame();
        }
        else
        {
            Debug.LogError("TakeWalkManager.Instance is null");
        }
        */
        // UI를 표시
        // TakeWalkUIManager.Instance.ShowGameOverUI();
    }

    /*
    void UpdateScore()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (distanceTravelled >= 10f)
        {
            TakeWalkUIManager.Instance.AddScore(10);
            distanceTravelled = 0f;
        }
    }
    */
}
