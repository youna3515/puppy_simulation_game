using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    UI_Scene _currentSceneUI;
    public UI_Scene CurrentSceneUI
    {
        get
        {
            return _currentSceneUI;
        }
        set
        {
            _currentSceneUI = value;
        }
    }

    public void ShowSceneUI(GameObject sceneUI)
    {
        ;
    }
}
