using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        distanceTraveled += Time.deltaTime * PlayerController.Instance.moveSpeed;
        TakeWalkUIManager.Instance.UpdateDistance(distanceTraveled);
        if (distanceTraveled >= maxDistance)
        {
            RewardSystem.Instance.GiveReward();
        }
    }

    public void EndGame()
    {
        if (distanceTraveled > bestDistance)
        {
            bestDistance = distanceTraveled;
            SaveManager.Instance.SaveBestDistance(bestDistance);
        }
        TakeWalkUIManager.Instance.ShowGameOver();
    }
}
