using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HouseScene : UI_Scene
{
    GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set { _player = value; }
    }

    PuppyVariable _puppyVariable;

    GameObject _uIDoTaskButtonGrid;

    bool _bIsDoTaskButtonExist = false;
    enum Buttons
    {
        ShowTaskButton,
        GameExitButton,
        WalkCounterButton
    }

    enum Sliders
    {
        CleanlinessBar,
        FullnessBar,
        StaminaBar,
        StressBar
    }

    void OnShowTaskButtonDown(PointerEventData pointerEventData)
    {
        GetUIObject<Button>((int)Buttons.ShowTaskButton).transform.Rotate(new Vector3(0, 0, 180));
        if (_bIsDoTaskButtonExist == false)
        {
            _bIsDoTaskButtonExist = true;
            _uIDoTaskButtonGrid = Managers.UIManager.ShowPopupUI<UI_DoTaskButtonGrid>().gameObject;
            _uIDoTaskButtonGrid.GetComponent<UI_DoTaskButtonGrid>().Player = _player;
        }
        else
        {
            _bIsDoTaskButtonExist = false;
            GameObject.Destroy(_uIDoTaskButtonGrid);
        }
    }

    void OnGameExitButtonDown(PointerEventData pointerEventData)
    {
        float currentUnixTime = (float)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Managers.DataManager.LastQuitTime = currentUnixTime;

        _puppyVariable.SaveVariables();

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    void OnWalkCounterButtonDown(PointerEventData pointerEventData)
    {
        _puppyVariable.SaveVariables();
        Managers.SceneManager.LoadScene("WalkCounterScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        _puppyVariable = Player.GetComponent<PuppyVariable>();

        SaveUIObjectByEnum<Button>(typeof(Buttons));
        SaveUIObjectByEnum<Slider>(typeof(Sliders));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.ShowTaskButton).gameObject, Defines.UIEventType.PointDown ,OnShowTaskButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.GameExitButton).gameObject, Defines.UIEventType.PointDown, OnGameExitButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.WalkCounterButton).gameObject, Defines.UIEventType.PointDown, OnWalkCounterButtonDown);
    }

    // Update is called once per frame
    void Update()
    {
        GetUIObject<Slider>((int)Sliders.CleanlinessBar).value = _puppyVariable.Cleanliness / 100.0f;
        GetUIObject<Slider>((int)Sliders.FullnessBar).value = _puppyVariable.Fullness / 100.0f;
        GetUIObject<Slider>((int)Sliders.StaminaBar).value = _puppyVariable.Stamina / 100.0f;
        GetUIObject<Slider>((int)Sliders.StressBar).value = _puppyVariable.Stress / 100.0f;
    }
}
