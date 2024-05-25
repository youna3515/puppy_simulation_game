/*
using UnityEngine;

public class TakeWalkManager : MonoBehaviour
{
    public static TakeWalkManager Instance;
    public float distanceTraveled = 0f;
    public float maxDistance = 3000f; // 3km
    private float bestDistance = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void EndGame()
    {
        if (distanceTraveled > bestDistance)
        {
            bestDistance = distanceTraveled;
            SaveManager.Instance.SaveBestDistance(bestDistance);
        }
        TakeWalkUIManager.Instance.ShowGameOverUI();
    }
}
*/