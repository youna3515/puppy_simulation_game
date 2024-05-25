/*
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TakeWalkUIManager : MonoBehaviour
{
    public static TakeWalkUIManager Instance;

    public Button retryButton;
    public Button backButton;
    public Image darkBackground; // 어두운 배경 이미지
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI currentScoreText;

    private int bestScore = 0;
    private int currentScore = 0;

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

        LoadBestScore();
    }

    void Start()
    {
        retryButton.onClick.AddListener(RetryGame);
        backButton.onClick.AddListener(BackToHome);
        HideGameOverUI();
        UpdateScoreUI();
    }

    void RetryGame()
    {
        HideGameOverUI();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드
    }

    void BackToHome()
    {
        //HideGameOverUI();
        UnityEngine.SceneManagement.SceneManager.LoadScene("HouseScene"); // 홈 씬으로 전환 (홈 씬 이름에 맞게 수정)
    }

    public void ShowGameOverUI()
    {
        darkBackground.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void HideGameOverUI()
    {
        darkBackground.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void UpdateScoreUI()
    {
        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + bestScore + " m";
        if (currentScoreText != null)
            currentScoreText.text = "Current Score: " + currentScore + " m";
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveBestScore();
        }
        UpdateScoreUI();

        // Debug.Log 추가
        Debug.Log("Added Score. Current Score: " + currentScore);
    }

    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
}
*/