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
        set { if (_player == null) _player = value; }
    }

    PuppyVariable _puppyVariable;

    [SerializeField] GameObject _uIDoTaskButtonGridPrefab;
    GameObject _uIDoTaskButtonGrid;

    bool _bIsDoTaskButtonExist = false;
    enum Buttons
    {
        ShowTaskButton
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
        if (_bIsDoTaskButtonExist == false)
        {
            _bIsDoTaskButtonExist = true;
            _uIDoTaskButtonGrid = Instantiate<GameObject>(_uIDoTaskButtonGridPrefab);
            _uIDoTaskButtonGrid.GetComponent<UI_DoTaskButtonGrid>().Player = _player;
        }
        else
        {
            _bIsDoTaskButtonExist = false;
            GameObject.Destroy(_uIDoTaskButtonGrid);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _puppyVariable = Player.GetComponent<PuppyVariable>();

        SaveUIObjectByEnum<Button>(typeof(Buttons));
        SaveUIObjectByEnum<Slider>(typeof(Sliders));

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.ShowTaskButton).gameObject, Defines.UIEventType.PointDown ,OnShowTaskButtonDown);
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
