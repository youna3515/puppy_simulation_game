using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopwatchManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Button startButton;
    public Sprite startSprite;
    public Sprite finishSprite;
    public GameObject corgiPrefab;
    public float corgiSpeed = 1f;

    public GameObject popupPanel; // 팝업 패널
    public TextMeshProUGUI popupTimeText; // 팝업에 표시될 시간 텍스트
    public Button continueButton; // 팝업의 Continue 버튼
    public Button finishButton; // 팝업의 Finish 버튼

    private float elapsedTime = 0f;
    private bool isRunning = false;
    private Animator corgiAnimator;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        finishButton.onClick.AddListener(OnFinishButtonClick);

        UpdateTimerText();

        // Animator 설정
        if (corgiPrefab != null)
        {
            corgiAnimator = corgiPrefab.GetComponent<Animator>();
            if (corgiAnimator == null)
            {
                Debug.LogError("Animator not found on Corgi Prefab.");
            }
        }

        popupPanel.SetActive(false); // 팝업 패널 비활성화
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        if (hours > 0)
        {
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void OnStartButtonClick()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            startButton.image.sprite = finishSprite;
            if (corgiAnimator != null)
            {
                corgiAnimator.SetFloat("Speed", corgiSpeed); // 애니메이션 속도를 설정하여 달리기 애니메이션 시작
                Debug.Log("Animator Speed: " + corgiAnimator.GetFloat("Speed")); // 디버그 코드 추가
            }
        }
        else
        {
            startButton.image.sprite = startSprite;
            if (corgiAnimator != null)
            {
                corgiAnimator.SetFloat("Speed", 0f); // 애니메이션 속도를 0으로 설정하여 멈추기
                Debug.Log("Animator Speed: " + corgiAnimator.GetFloat("Speed")); // 디버그 코드 추가
            }
            ShowPopup(); // 팝업 표시
        }
    }

    void ShowPopup()
    {
        popupPanel.SetActive(true);
        popupTimeText.text = "Today's walk: " + timerText.text; // 팝업에 전체 문구 표시
    }

    public void OnContinueButtonClick()
    {
        popupPanel.SetActive(false); // 팝업 패널 숨기기
        isRunning = true;
        startButton.image.sprite = finishSprite;
        if (corgiAnimator != null)
        {
            corgiAnimator.SetFloat("Speed", corgiSpeed); // 애니메이션 속도를 설정하여 달리기 애니메이션 시작
        }
    }

    public void OnFinishButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HouseScene"); // HouseScene으로 씬 전환
    }
}
