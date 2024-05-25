using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Warning : UI_Popup
{
    enum Buttons
    {
        OKButton
    }

    enum Texts
    {
        LeftChanceText
    }

    void OnOKButtonDown(PointerEventData data)
    {
        Time.timeScale = 1.0f;
        GameObject.Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        SaveUIObjectByEnum<Button>(typeof(Buttons));
        SaveUIObjectByEnum<Text>(typeof(Texts));

        GetUIObject<Text>((int)Texts.LeftChanceText).text = $"Chance Decrease\r\n{Managers.DataManager.Chance} Chance Left";
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.OKButton).gameObject, Defines.UIEventType.PointDown, OnOKButtonDown);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
