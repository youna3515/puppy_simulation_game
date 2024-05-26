using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    int _takeWalkStartInterval = 3;
    public int TakeWalkStartInterval
    {
        get
        {
            return _takeWalkStartInterval;
        }
    }

    public float LastQuitTime
    {
        get
        {
            return PlayerPrefs.GetFloat("LastQuitTime");
        }
        set
        {
            float currentUnixTime = value;
            PlayerPrefs.SetFloat("LastQuitTime", currentUnixTime);
        }
    }

    public float Stress
    {
        get
        {
            return PlayerPrefs.GetFloat("Stress");
        }
        set
        {
            float currentStress = value;
            PlayerPrefs.SetFloat("Stress", currentStress);
        }
    }

    public float Stamina
    {
        get
        {
            return PlayerPrefs.GetFloat("Stamina");
        }
        set
        {
            float currentStamina = value;
            PlayerPrefs.SetFloat("Stamina", currentStamina);
        }
    }

    public float Fullness
    {
        get
        {
            return PlayerPrefs.GetFloat("Fullness");
        }
        set
        {
            float currentFullness = value;
            PlayerPrefs.SetFloat("Fullness", currentFullness);
        }
    }

    public float Cleanliness
    {
        get
        {
            return PlayerPrefs.GetFloat("Cleanliness");
        }
        set
        {
            float currentCleanliness = value;
            PlayerPrefs.SetFloat("Cleanliness", currentCleanliness);
        }
    }

    public float Toilet
    {
        get
        {
            return PlayerPrefs.GetFloat("Toilet");
        }
        set
        {
            float currentToilet = value;
            PlayerPrefs.SetFloat("Toilet", currentToilet);
        }
    }

    public int Chance
    {
        get
        {
            return PlayerPrefs.GetInt("Chance", 0);
        }
        set
        {
            int currentChance = value;
            PlayerPrefs.SetInt("Chance", currentChance);
        }
    }

    public int WalkCount
    {
        get
        {
            return PlayerPrefs.GetInt("WalkCount", 0);
        }
        set
        {
            int currentWalkCount = value;
            PlayerPrefs.SetInt("WalkCount", currentWalkCount);
        }
    }
}
