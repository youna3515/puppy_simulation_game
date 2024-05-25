using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalHalfScreenRecognizer : MonoBehaviour
{
    public Action LeftHalfAction = null;
    public Action RightHalfAction = null;

    // Start is called before the first frame update
    void Start()
    {
        Managers.InputManager.PointerDownInputAction -= VerticalHalfRecognizer;
        Managers.InputManager.PointerDownInputAction += VerticalHalfRecognizer;
    }

    void VerticalHalfRecognizer(Vector3 inputPosition)
    {
        if (inputPosition.x < Screen.width / 2)
        {
            LeftHalfAction.Invoke();
        }
        else
        {
            RightHalfAction.Invoke();
        }
    }
}
