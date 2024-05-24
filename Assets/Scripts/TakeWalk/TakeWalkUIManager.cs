using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeWalkUIManager : MonoBehaviour
{
    public static TakeWalkUIManager Instance;
    public Text distanceText;
    public Text bestDistanceText;
    public GameObject gameOverPanel;

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
    }

    public void UpdateDistance(float distance)
    {
        distanceText.text = "Distance: " + distance.ToString("F2") + " m";
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
