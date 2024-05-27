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

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (name == null)
        {
            name = typeof(T).Name;
        }
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/PopupUI/" + name);
        if (prefab != null)
        {
            GameObject go = Object.Instantiate<GameObject>(prefab);
            return go.GetComponent<T>();
        }
        return null;
    }
}
