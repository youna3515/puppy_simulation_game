using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Defines;
using static PuppyController;

public class UI_DoTaskButtonGrid : UI_Popup
{
    GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set { _player = value; }
    }

    PuppyVariable _variable;

    enum Buttons
    {
        EatButton,
        TakeWalkButton,
        GoToiletButton,
        SleepButton,
        TakeWashButton
    }

    void OnEatButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.EatTask);
    }

    void OnTakeWalkButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.TakeWalkTask);
    }

    void OnGoToiletButtonDown(PointerEventData data)
    {
        float rndVal = UnityEngine.Random.Range(0.0f, 100.0f);
        Debug.Log(rndVal);
        if (_variable.CorrectToiletRatio < rndVal)
        {
            _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.GoWrongToiletTask);
        }
        else
        {
            _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.GoToiletTask);
        }
    }

    void OnSleepButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.SleepTask);
    }

    void OnTakeWashButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(PuppyTaskType.TakeWashTask);
    }

    // Start is called before the first frame update
    void Start()
    {
        _variable = _player.GetComponent<PuppyVariable>();

        SaveUIObjectByEnum<Button>(typeof(Buttons));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.EatButton).gameObject, Defines.UIEventType.PointDown, OnEatButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.TakeWalkButton).gameObject, Defines.UIEventType.PointDown, OnTakeWalkButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.GoToiletButton).gameObject, Defines.UIEventType.PointDown, OnGoToiletButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.SleepButton).gameObject, Defines.UIEventType.PointDown, OnSleepButtonDown);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.TakeWashButton).gameObject, Defines.UIEventType.PointDown, OnTakeWashButtonDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
