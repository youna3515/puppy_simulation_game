using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TodayWalk : UI_Popup
{
    enum TextMeshProUGUIs
    {
        PopupTimeText
    }
    enum Buttons
    {
        ContinueButton,
        FinishButton
    }

    int _walkCount;
    public int WalkCount
    {
        get
        {
            return _walkCount;
        }
        set
        {
            _walkCount = value;
        }
    }
    string _timerText;

    public string TimerText
    {
        set
        {
            _timerText = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveUIObjectByEnum<Button>(typeof(Buttons));
        SaveUIObjectByEnum<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.FinishButton).gameObject, Defines.UIEventType.PointDown, OnFinishButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.ContinueButton).gameObject, Defines.UIEventType.PointDown, OnContinueButtonDown);

        GetUIObject<TextMeshProUGUI>((int)TextMeshProUGUIs.PopupTimeText).text = "Total Time : " + _timerText + "\nTotal Walk : " + WalkCount;
    }

    void OnContinueButtonDown(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        GameObject.Destroy(gameObject);
    }

    void OnFinishButtonDown(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        Managers.DataManager.Stress -= (float)WalkCount / 3000.0f;
        Managers.SceneManager.LoadScene("HouseScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
