using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject _startSceneUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (_startSceneUIPrefab != null)
        {
            UI_TakeWalkScene _takeWalkSceneUI = Instantiate<GameObject>(_startSceneUIPrefab).GetComponent<UI_TakeWalkScene>();
            Managers.UIManager.CurrentSceneUI = _takeWalkSceneUI;
        }
    }

}
