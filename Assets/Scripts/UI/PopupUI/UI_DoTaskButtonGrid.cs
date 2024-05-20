using System.Collections;
using System.Collections.Generic;
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
        set { if (_player == null) _player = value; }
    }

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
        _player.GetComponent<PuppyTask>().DoTask(DogTask.EatTask);
    }

    void OnTakeWalkButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(DogTask.TakeWalkTask);
    }

    void OnGoToiletButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(DogTask.GoToiletTask);
    }

    void OnSleepButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(DogTask.SleepTask);
    }

    void OnTakeWashButtonDown(PointerEventData data)
    {
        _player.GetComponent<PuppyTask>().DoTask(DogTask.TakeWashTask);
    }

    // Start is called before the first frame update
    void Start()
    {
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
