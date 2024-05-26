using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCounterScene : MonoBehaviour
{
    [SerializeField] GameObject _puppyPrefab;
    [SerializeField] GameObject _walkCounterSceneUIPrefab;
    GameObject _puppy;
    public GameObject Puppy
    {
        get
        {
            return _puppy;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_puppyPrefab != null)
        {
            _puppy = Instantiate<GameObject>(_puppyPrefab);
        }

        if (_walkCounterSceneUIPrefab != null)
        {
            Managers.UIManager.CurrentSceneUI = Instantiate<GameObject>(_walkCounterSceneUIPrefab).GetComponent<UI_Scene>();
        }
    }
}
