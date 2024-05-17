using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScene : MonoBehaviour
{
    [SerializeField] GameObject _dogPrefab;
    [SerializeField] GameObject _houseSceneUIPrefab;

    DogController _dog;
    public DogController Dog
    {
        get { return _dog; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (_dogPrefab != null)
        {
            _dog = Instantiate<GameObject>(_dogPrefab).GetComponent<DogController>();
            Camera.main.GetComponent<FollowTarget>().Target = _dog.transform;
        }

        if (_houseSceneUIPrefab != null)
        {
            Managers.UIManager.CurrentSceneUI = Instantiate<GameObject>(_houseSceneUIPrefab).GetComponent<UI_Scene>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
