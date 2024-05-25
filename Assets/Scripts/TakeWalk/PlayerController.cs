using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float moveSpeed = 10f; // 이동 속도
    public float animationSpeed = 7f; // 애니메이션 속도
    public Camera mainCamera;

    private Vector3 targetPosition; // 목표 위치
    private bool isRunning = false;
    private Animator animator;
    public BackgroundScroller backgroundScroller; // 배경 스크롤러 참조

    private float screenLeftEdge;
    private float screenRightEdge;
    private float roadWidth; // RoadManager에서 설정된 길의 폭

    private float distanceTravelled = 0f;
    private Vector3 lastPosition;

    private Coroutine currentMoveCoroutine; // 현재 이동 코루틴을 추적

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

    public void SetRoadWidth(float width)
    {
        roadWidth = width + 4;
    }

    void Start()
    {
        StartCoroutine(StartCountdown());

        // 화면 경계 계산
        float cameraZDistance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        screenLeftEdge = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraZDistance)).x;
        screenRightEdge = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, cameraZDistance)).x;

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
        animator.SetFloat("Speed", animationSpeed); // 카운트다운 후 애니메이션 속도 설정하여 달리기 시작

        // 배경 스크롤러 설정
        if (backgroundScroller != null)
        {
            backgroundScroller.scrollSpeed = moveSpeed;
        }
    }

    void Update()
    {
        if (isRunning)
        {
            HandleTouchInput();
            MoveForward();
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

    IEnumerator MoveInDirection(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.3f)
        {
            // 목표 방향으로 부드럽게 이동
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            float halfRoadWidth = roadWidth / 2;
            newPosition.x = Mathf.Clamp(newPosition.x, -halfRoadWidth, halfRoadWidth);

            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;
        float halfRoadWidth = roadWidth / 2;
        position.x = Mathf.Clamp(position.x, -halfRoadWidth, halfRoadWidth);
        transform.position = position;
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", animationSpeed); // 항상 animationSpeed 값을 사용하여 Speed 파라미터 설정
        }
    }

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
        moveSpeed = 0;
        animationSpeed = 0;

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
