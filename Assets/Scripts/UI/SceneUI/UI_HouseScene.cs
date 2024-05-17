using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HouseScene : UI_Scene
{
    [SerializeField] GameObject _uIDoTaskButtonGridPrefab;

    GameObject _uIDoTaskButtonGrid;
    bool _bIsDoTaskButtonExist = false;
    enum Buttons
    {
        ShowTaskButton
    }

    void OnShowTaskButtonDown(PointerEventData pointerEventData)
    {
        if (_bIsDoTaskButtonExist == false)
        {
            _bIsDoTaskButtonExist = true;
            _uIDoTaskButtonGrid = Instantiate<GameObject>(_uIDoTaskButtonGridPrefab);
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
        SaveUIObjectByEnum<Button>(typeof(Buttons));
        GetUIObject<Button>((int)Buttons.ShowTaskButton);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.ShowTaskButton).gameObject, Defines.UIEventType.PointDown ,OnShowTaskButtonDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
