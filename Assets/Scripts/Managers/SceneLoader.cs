using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 네임스페이스 추가

public class SceneLoader : MonoBehaviour
{
    public void LoadTakeWalkScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TakeWalk");
    }
}
