using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, List<UnityEngine.Object>> _uIObjectDic = new Dictionary<Type, List<UnityEngine.Object>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SaveUIObjectByEnum<T>(Type enumType, bool bFindDirectChildOnly = false) where T : UnityEngine.Object
    {
        string[] UIObjectNameArr = Enum.GetNames(enumType);
        List<UnityEngine.Object> uIObjectList = new List<UnityEngine.Object>();
        _uIObjectDic.Add(typeof(T), uIObjectList);

        foreach (string uIObjectName in UIObjectNameArr)
        {
            if (typeof(T) == typeof(GameObject))
            {
                if (bFindDirectChildOnly)
                {
                    uIObjectList.Add(transform.Find(uIObjectName).gameObject);
                }
                else
                {
                    RectTransform[] uIObjectRectTransformArr = gameObject.GetComponentsInChildren<RectTransform>();
                    foreach (RectTransform uIObjectRectTransform in uIObjectRectTransformArr)
                    {
                        if (uIObjectRectTransform.gameObject.name == uIObjectName)
                        {
                            uIObjectList.Add(uIObjectRectTransform.gameObject);
                        }
                    }
                }
                
            }
            else
            {
                if (bFindDirectChildOnly)
                {
                    uIObjectList.Add(transform.Find(uIObjectName).GetComponent<T>());
                }
                else
                {
                    RectTransform[] uIObjectRectTransformArr = gameObject.GetComponentsInChildren<RectTransform>();
                    foreach (RectTransform uIObjectRectTransform in uIObjectRectTransformArr)
                    {
                        if (uIObjectRectTransform.gameObject.name == uIObjectName)
                        {
                            uIObjectList.Add(uIObjectRectTransform.GetComponent<T>());
                        }
                    }
                }
            }
        }    
    }

    protected T GetUIObject<T>(int enumNum) where T : UnityEngine.Object
    {
        List<UnityEngine.Object> uIObjectList;
        _uIObjectDic.TryGetValue(typeof(T), out uIObjectList);
        if ( uIObjectList == null )
        {
            Debug.Log($"Can't Find UIList {typeof(T)}");
        }
        return (T)uIObjectList[enumNum];
    }

    protected void BindFuntionToHandler(GameObject uIObject, Defines.UIEventType uIEventType, Action<PointerEventData> FunctionToBind)
    {
        UI_EventHandler uI_EventHandler = uIObject.GetComponent<UI_EventHandler>();
        if (uI_EventHandler == null)
        {
            uI_EventHandler = uIObject.AddComponent<UI_EventHandler>();
        }

        switch (uIEventType)
        {
            case Defines.UIEventType.PointDown:
                uI_EventHandler.PointerDownAction -= FunctionToBind;
                uI_EventHandler.PointerDownAction += FunctionToBind;
                break;
            case Defines.UIEventType.PointUp:
                uI_EventHandler.PointerUpAction -= FunctionToBind;
                uI_EventHandler.PointerUpAction += FunctionToBind;
                break;
        }
    }
}
