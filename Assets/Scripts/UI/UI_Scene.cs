using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.UIManager.CurrentSceneUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
