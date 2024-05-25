using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Buttons
    {
        RestartButton
    }

    void OnRestartButtonDown(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        Managers.DataManager.Chance = 0;
        Managers.SceneManager.LoadScene("StartScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        SaveUIObjectByEnum<Button>(typeof(Buttons));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.RestartButton).gameObject, Defines.UIEventType.PointDown, OnRestartButtonDown);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
