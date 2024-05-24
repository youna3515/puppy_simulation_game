using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI CurrentScoreText;


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
        UpdateScoreUI();
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
    }

    void UpdateScoreUI()
    {
        BestScoreText.text = "Best Score: " + bestScore + " m";
        CurrentScoreText.text = "Current Score: " + currentScore + " m";
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
