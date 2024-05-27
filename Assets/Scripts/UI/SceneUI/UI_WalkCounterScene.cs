using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_WalkCounterScene : UI_Scene
{
    enum TextMeshProUGUIs
    {
        CurrentTimeText,
        WalkCountText
    }
    enum Buttons
    {
        StartButton
    }

    bool _isRunning = false;
    float _elapsedTime = 0f;
    [SerializeField] Sprite _beforeStartImage;
    [SerializeField] Sprite _afterStartImage;



    public Action OnStartTimerAction;

    WalkCounter _walkCounter;

    // Start is called before the first frame update
    void Start()
    {
        _walkCounter = GetComponent<WalkCounter>();

        SaveUIObjectByEnum<Button>(typeof(Buttons));
        SaveUIObjectByEnum<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.StartButton).gameObject, Defines.UIEventType.PointDown, OnStartButtonDown);

    }

    void OnStartButtonDown(PointerEventData data)
    {
        if (!_isRunning)
        {
            GetUIObject<Button>((int)Buttons.StartButton).image.sprite = _afterStartImage;
            OnStartTimerAction.Invoke();
            _isRunning = true;
        }
        else
        {
            UI_TodayWalk popupUI = Managers.UIManager.ShowPopupUI<UI_TodayWalk>();
            popupUI.WalkCount = _walkCounter.StepCount;
            popupUI.TimerText = GetUIObject<TextMeshProUGUI>((int)TextMeshProUGUIs.CurrentTimeText).text;
            Time.timeScale = 0.0f;
        }
    }

    void Update()
    {
        if (_isRunning)
        {
            _elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
        GetUIObject<TextMeshProUGUI>((int)TextMeshProUGUIs.WalkCountText).text = $"Walk Count : {_walkCounter.StepCount}";
    }

    void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(_elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((_elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);

        if (hours > 0)
        {
            GetUIObject<TextMeshProUGUI>((int)TextMeshProUGUIs.CurrentTimeText).text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            GetUIObject<TextMeshProUGUI>((int)TextMeshProUGUIs.CurrentTimeText).text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
