using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_WalkSuccess : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveUIObjectByEnum<Button>(typeof(Buttons));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.CloseButton).gameObject, Defines.UIEventType.PointDown, OnCloseButtonDown);
    }

    private void OnCloseButtonDown(PointerEventData data)
    {
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
