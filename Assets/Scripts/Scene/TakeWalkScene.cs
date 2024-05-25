using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TakeWalkScene : MonoBehaviour
{
    [SerializeField] GameObject _puppyPrefab;
    [SerializeField] GameObject _takeWalkSceneUIPrefab;
    [SerializeField] GameObject _roadSpawnerPrefab;

    GameObject _puppy;
    public GameObject Puppy
    {
        get { return _puppy; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (_puppyPrefab != null)
        {
            _puppy = Instantiate<GameObject>(_puppyPrefab);
            Camera.main.GetComponent<FollowTarget>().Target = _puppy.transform;
        }

        if (_takeWalkSceneUIPrefab != null)
        {
            UI_TakeWalkScene _takeWalkSceneUI = Instantiate<GameObject>(_takeWalkSceneUIPrefab).GetComponent<UI_TakeWalkScene>();
            _takeWalkSceneUI.Player = _puppy;
            Managers.UIManager.CurrentSceneUI = _takeWalkSceneUI;
        }

        if (_roadSpawnerPrefab != null)
        {
            RoadSpawner roadSpawner = Instantiate<GameObject>(_roadSpawnerPrefab).GetComponent<RoadSpawner>();
            roadSpawner.Player = _puppy.transform;

        }
    }
}
