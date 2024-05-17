using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool _bIsPointerDown = false;

    public Action<PointerEventData> PointerDownAction = null;
    public Action<PointerEventData> PointerUpAction = null;
    public Action PointerPressedAction = null;
    public void OnPointerDown(PointerEventData eventData)
    {
        _bIsPointerDown = true;
        if (PointerDownAction != null)
        {
            PointerDownAction.Invoke(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _bIsPointerDown = false;
        if (PointerUpAction != null)
        {
            PointerUpAction.Invoke(eventData);
        }
    }

    void OnPointerPressed()
    {
        if (PointerPressedAction != null)
        {
            PointerPressedAction.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_bIsPointerDown == true)
        {
            OnPointerPressed();
        }
    }
}
