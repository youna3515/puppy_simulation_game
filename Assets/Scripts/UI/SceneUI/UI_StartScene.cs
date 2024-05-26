using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StartScene : UI_Scene
{
    enum Buttons
    {
        TimerButton,
        HouseButton
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveUIObjectByEnum<Button>(typeof(Buttons));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.HouseButton).gameObject, Defines.UIEventType.PointDown, GoHouseScene);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.TimerButton).gameObject, Defines.UIEventType.PointDown, GoTimerScene);
    }

    void GoHouseScene(PointerEventData data)
    {
        Managers.SceneManager.LoadScene("HouseScene");
    }

    void GoTimerScene(PointerEventData data)
    {
        Managers.SceneManager.LoadScene("WalkCounterScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
