using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScene : MonoBehaviour
{
    [SerializeField] GameObject _dogPrefab;
    [SerializeField] GameObject _roomPrefab;
    [SerializeField] GameObject _houseSceneUI;

    DogController _dog;
    public DogController Dog
    {
        get { return _dog; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_dogPrefab != null)
        {
            _dog = Instantiate<GameObject>(_dogPrefab).GetComponent<DogController>();
            if (_dog == null)
            {
                Debug.LogWarning("Dog NULL");
            }
            Camera.main.GetComponent<FollowTarget>().Target = _dog.transform;
        }

        if (_roomPrefab != null)
        {
            Instantiate<GameObject>(_roomPrefab);
        }

        if (_houseSceneUI != null)
        {
            Managers.UIManager.CurrentSceneUI = Instantiate<GameObject>(_houseSceneUI).GetComponent<UI_Scene>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
