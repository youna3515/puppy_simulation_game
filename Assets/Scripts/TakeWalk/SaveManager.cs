/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

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

    public void SaveBestDistance(float bestDistance)
    {
        PlayerPrefs.SetFloat("BestDistance", bestDistance);
    }

    public float LoadBestDistance()
    {
        return PlayerPrefs.GetFloat("BestDistance", 0);
    }
}
*/
