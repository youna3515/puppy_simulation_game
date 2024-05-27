using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public void LoadScene(string sceneName)
    {
        Managers.InputManager.PointerDownInputAction = null;
        SceneManager.LoadScene(sceneName);
    }
}
