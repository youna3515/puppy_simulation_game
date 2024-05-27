using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HouseScene : MonoBehaviour
{
    [SerializeField] GameObject _puppyPrefab;
    [SerializeField] GameObject _RoomPrefab;
    [SerializeField] GameObject _houseSceneUIPrefab;
    [SerializeField] NavMeshData _navMeshData;

    GameObject _puppy;
    public GameObject Puppy
    {
        get { return _puppy; }
    }

    GameObject _room;
    public GameObject Room
    {
        get { return _room; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Managers.DataManager.Chance == -1)
        {
            Debug.Log("신규 세션입니다");
            Managers.DataManager.InitVariables();
        }

        if (_houseSceneUIPrefab != null)
        {
            _room = Instantiate<GameObject>(_RoomPrefab);
            NavMeshSurface navMeshSurface = _room.GetComponentInChildren<NavMeshSurface>();
            navMeshSurface.navMeshData = _navMeshData;
        }

        if (_puppyPrefab != null)
        {
            _puppy = Instantiate<GameObject>(_puppyPrefab);
            _puppy.transform.position = new Vector3(0, 0.5f, 0);
            _puppy.GetComponent<PuppyTask>().TaskPoint = _room.transform.Find("TaskPoint").gameObject;
            Camera.main.GetComponent<FollowTarget>().Target = _puppy.transform;
        }

        if (_houseSceneUIPrefab != null)
        {
            UI_HouseScene houseSceneUI = Instantiate<GameObject>(_houseSceneUIPrefab).GetComponent<UI_HouseScene>();
            houseSceneUI.Player = _puppy.gameObject;
            Managers.UIManager.CurrentSceneUI = houseSceneUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
