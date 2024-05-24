using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 네임스페이스 추가

public class SceneLoader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }

    public void LoadHomeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HouseScene");
    }
    public void LoadTakeWalkScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TakeWalk");
    }

}
