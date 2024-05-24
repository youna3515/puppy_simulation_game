using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float moveSpeed = 5f;
    public float turnSpeed = 10f; // 회전 속도
    public float forwardSpeed = 10f; // 앞으로 이동하는 속도
    public Camera mainCamera;

    private Vector3 targetDirection;
    private bool isTurning = false;
    private Animator animator;
    private bool isRunning = false;
    public BackgroundScroller backgroundScroller; // 배경 스크롤러 참조

    private float screenLeftEdge;
    private float screenRightEdge;

    private float distanceTravelled = 0f;
    private Vector3 lastPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(StartCountdown());

        // 화면 경계 계산
        float cameraZDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        screenLeftEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraZDistance)).x-3;
        screenRightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, cameraZDistance)).x+3;

        lastPosition = transform.position;
    }
    IEnumerator StartCountdown()
    {
        // 카운트다운 텍스트를 화면에 표시
        CountdownTextController countdownText = FindObjectOfType<CountdownTextController>();
        if (countdownText != null)
        {
            countdownText.StartCountdown();
        }

        // 게임 시작 전 카메라 위치 초기화
        UpdateCameraPosition();

        yield return new WaitForSeconds(3); // 3초 카운트다운
        isRunning = true;
        animator.SetFloat("Speed", moveSpeed); // 카운트다운 후 Speed 값을 moveSpeed로 설정하여 달리기 시작

        if (backgroundScroller != null)
        {
            backgroundScroller.scrollSpeed = moveSpeed; // 배경 스크롤 속도 설정
        }
    }



    void Update()
    {
        if (isRunning)
        {
            HandleTouchInput();
            MoveForward();
            if (isTurning)
            {
                TurnTowardsTarget();
            }
            ClampPosition();
            UpdateAnimator();
            UpdateCameraPosition();
            UpdateScore();
        }
    }

    void HandleTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inputPosition = Input.mousePosition;
            if (inputPosition.x < Screen.width / 2)
            {
                // 화면의 왼쪽 클릭
                SetTargetDirection(Vector3.left + Vector3.forward);
            }
            else
            {
                // 화면의 오른쪽 클릭
                SetTargetDirection(Vector3.right + Vector3.forward);
            }
        }
    }

    void SetTargetDirection(Vector3 direction)
    {
        targetDirection = direction.normalized;
        isTurning = true;
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void TurnTowardsTarget()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, turnSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if (Vector3.Angle(transform.forward, targetDirection) < 0.1f)
        {
            isTurning = false;
        }
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;
        if (position.x < screenLeftEdge)
        {
            position.x = screenLeftEdge;
            targetDirection = Vector3.forward;
        }
        else if (position.x > screenRightEdge)
        {
            position.x = screenRightEdge;
            targetDirection = Vector3.forward;
        }
        transform.position = position;
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", moveSpeed); // 항상 moveSpeed 값을 사용하여 Speed 파라미터 설정
        }
    }

void UpdateCameraPosition()
{
    if (mainCamera != null)
    {
        Vector3 newCameraPosition = mainCamera.transform.position;
        newCameraPosition.x = transform.position.x; // 카메라의 X 위치를 강아지의 X 위치로 설정
        //newCameraPosition.y = transform.position.y + 15; // 카메라를 위로 더 올려서 강아지를 내려다보게 설정
        newCameraPosition.y = 5;
        newCameraPosition.z = transform.position.z-5; // 카메라와 플레이어의 Z 거리 유지
        mainCamera.transform.position = newCameraPosition;
        //mainCamera.transform.LookAt(transform.position + new Vector3(0, 10, 0)); // 카메라가 강아지보다 약간 위를 보도록 설정
    }
}




    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with obstacle");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        // 강아지의 애니메이터에서 Die 트리거 설정
        animator.SetTrigger("Die");

        // 게임 종료 로직 (예: 플레이어의 움직임 멈추기 등)
        isRunning = false;
        forwardSpeed = 0;
        moveSpeed = 0;

        yield return new WaitForSeconds(2); // 2초 대기

        // TakeWalkManager 인스턴스가 null인지 확인
        if (TakeWalkManager.Instance != null)
        {
            TakeWalkManager.Instance.EndGame();
        }
        else
        {
            Debug.LogError("TakeWalkManager.Instance is null");
        }

        // UI를 표시
        TakeWalkUIManager.Instance.ShowGameOverUI();
    }

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
}
