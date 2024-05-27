using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyVariable : MonoBehaviour
{
    [SerializeField] float _stress = 0.0f; //스트레스
    public float Stress { get { return _stress; } }
    [SerializeField] float _stamina = 100f; //체력
    public float Stamina { get { return _stamina; } }
    [SerializeField] float _fullness = 100f; //배부름
    public float Fullness { get { return _fullness; } }
    [SerializeField] float _cleanliness = 100f; //청결도
    public float Cleanliness { get { return _cleanliness; } }
    [SerializeField] float _toilet = 0.0f;
    public float Toilet { get { return _toilet; } }

    [SerializeField] int _chance = 3; //기회(목숨)
    public int Chance { get { return _chance; } }

    [SerializeField] float _correctToiletRatio = 50.0f;
    public float CorrectToiletRatio { get { return _correctToiletRatio; } }

    [SerializeField] float _fullnessDecreaseRate = 1.0f;
    [SerializeField] float _cleanlinessDecreaseRate = 1.0f;
    [SerializeField] float _staminaDecreaseRate = 1.0f;

    [SerializeField] float _stressIncreaseRateByStamina = 0.0f;
    [SerializeField] float _stressIncreaseRateByFullness = 0.0f;
    [SerializeField] float _stressIncreaseRateByCleanliness = 0.0f;
    [SerializeField] float _stressIncreaseRateByToilet = 0.0f;

    bool _isTrainable = false;

    private void Start()
    {
        if (Managers.DataManager.LastQuitTime <= 0)
        {
            Debug.Log("첫 접속");
            Managers.DataManager.InitVariables();
        }
        float currentUnixTime = (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        float timeInterval = currentUnixTime - Managers.DataManager.LastQuitTime;
        Managers.DataManager.LastQuitTime = currentUnixTime;

        _stress = Managers.DataManager.Stress;
        _stamina = Managers.DataManager.Stamina * (1 - (timeInterval / 86400.0f));
        _fullness = Managers.DataManager.Fullness * (1 - (timeInterval / 86400.0f));
        _cleanliness = Managers.DataManager.Cleanliness * (1 - (timeInterval / 86400.0f));
        _toilet = Managers.DataManager.Toilet * (1 + (timeInterval / 86400.0f));
        _chance = Managers.DataManager.Chance;
    }

    void UpdateFullness()
    {
        _fullness -= _fullnessDecreaseRate * Time.deltaTime;
        _fullness = Mathf.Clamp(_fullness, 0f, 100f);
    }

    void UpdateCleanliness()
    {
        _cleanliness -= _cleanlinessDecreaseRate * Time.deltaTime;
        _cleanliness = Mathf.Clamp(_cleanliness, 0f, 100f);
    }

    void UpdateStamina()
    {
        _stamina -= _staminaDecreaseRate * Time.deltaTime;
        _stamina = Mathf.Clamp(_stamina, 0f, 100f);
    }

    void UpdateToliet()
    {
        _toilet += Time.deltaTime;
        _toilet = Mathf.Clamp(_toilet, 0f, 100f);
    }

    void UpdateStress()
    {
        if (_fullness <= 0f)
        {
            _stressIncreaseRateByFullness = 6.0f;
        }
        else if (_fullness <= 10f)
        {
            _stressIncreaseRateByFullness = 3.0f;
        }
        else if (_fullness <= 20f)
        {
            _stressIncreaseRateByFullness = 2.0f;
        }
        else if (_fullness <= 30f)
        {
            _stressIncreaseRateByFullness = 1.0f;
        }
        else
        {
            _stressIncreaseRateByFullness = 0.0f;
        }

        if (_cleanliness <= 0f)
        {
            _stressIncreaseRateByCleanliness = 4.0f;
        }
        else if (_cleanliness <= 10f)
        {
            _stressIncreaseRateByCleanliness = 2.0f;
        }
        else if (_cleanliness <= 20f)
        {
            _stressIncreaseRateByCleanliness = 1.0f;
        }
        else
        {
            _stressIncreaseRateByCleanliness = 0.0f;
        }

        if (_stamina <= 0f)
        {
            _stressIncreaseRateByStamina = 6.0f;
        }
        else if (_stamina <= 10f)
        {
            _stressIncreaseRateByStamina = 3.0f;
        }
        else if (_stamina <= 20f)
        {
            _stressIncreaseRateByStamina = 2.0f;
        }
        else if (_stamina <= 30f)
        {
            _stressIncreaseRateByStamina = 1.0f;
        }
        else
        {
            _stressIncreaseRateByStamina = 0.0f;
        }

        if (Toilet >= 100.0f)
        {
            _stressIncreaseRateByToilet = 6.0f;
        }
        else if (Toilet >= 80.0f)
        {
            _stressIncreaseRateByToilet = 3.0f;
        }
        else if (Toilet >= 50.0f)
        {
            _stressIncreaseRateByToilet = 1.0f;
        }
        else
        {
            _stressIncreaseRateByToilet = 0.0f;
        }

        _stress += (_stressIncreaseRateByFullness + _stressIncreaseRateByCleanliness + _stressIncreaseRateByStamina + _stressIncreaseRateByToilet) * Time.deltaTime;
        if (_stress >= 100.0f)
        {
            DecreaseChance();
        }
        _stress = Mathf.Clamp(_stress, 0.0f, 100.0f);
    }

    private void Update()
    {
        UpdateFullness();
        UpdateCleanliness();
        UpdateStamina();
        UpdateStress();
        UpdateToliet();
    }

    public void DecreaseChance()
    {
        OnChanceDecrease();
        if (_chance < 0)
        {
            Debug.Log("Game Over");
            Managers.UIManager.ShowPopupUI<UI_GameOver>();
        }
        else
        {
            _stress = 50.0f;
            Managers.UIManager.ShowPopupUI<UI_Warning>();
        }
    }

    public void Eat()
    {
        _fullness += 25.0f;
        Mathf.Clamp(_fullness, 0.0f, 100.0f);

        if (_isTrainable == true)
        {
            _correctToiletRatio += 10.0f;
        }
    }

    public void Wash()
    {
        _cleanliness += 50.0f;
        Mathf.Clamp(_cleanliness, 0.0f, 100.0f);
    }

    public void Sleep()
    {
        _stamina += 50.0f;
        Mathf.Clamp(_stamina, 0.0f, 100.0f);
    }

    public void GoToilet()
    {
        _toilet = 0;
        StartCoroutine(SetTrainable());
    }

    public void TakeWalk()
    {
        SaveVariables();
        Managers.SceneManager.LoadScene("TakeWalkScene");
    }

    public void GoWrongToilet()
    {
        _toilet = 0;
        _cleanliness -= 30.0f;
    }

    public void SaveVariables()
    {
        Managers.DataManager.Stress = Stress;
        Managers.DataManager.Cleanliness = Cleanliness;
        Managers.DataManager.Stamina = Stamina;
        Managers.DataManager.Fullness = Fullness;
        Managers.DataManager.Chance = Chance;
        Managers.DataManager.Toilet = Toilet;
        Managers.DataManager.CorrectToiletRatio = CorrectToiletRatio;
    }

    public void OnChanceDecrease()
    {
        _stress /= 2.0f;
        _cleanliness = 50.0f;
        _stamina = 50.0f;
        _fullness = 50.0f;
        _toilet /= 50.0f;
        _chance--;
        Managers.DataManager.Chance = _chance;

        Managers.DataManager.Stress = Stress;
        Managers.DataManager.Cleanliness = Cleanliness;
        Managers.DataManager.Stamina = Stamina;
        Managers.DataManager.Fullness = Fullness;
        Managers.DataManager.Toilet = Toilet;
    }

    IEnumerator SetTrainable()
    {
        _isTrainable = true;
        yield return new WaitForSeconds(30.0f);
        _isTrainable = false;
    }

    void OnApplicationQuit()
    {
        SaveVariables();
    }
}
