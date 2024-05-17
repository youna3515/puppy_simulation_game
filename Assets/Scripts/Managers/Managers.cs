using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers instance
    {
        get
        {
            if (s_instance == null)
            {
                GameObject managers = new GameObject();
                managers.name = "@Managers";
                DontDestroyOnLoad(managers);
                s_instance = managers.AddComponent<Managers>();
            }
            return s_instance;
        }
    }

    UIManager uIManager = new UIManager();
    public static UIManager UIManager
    {
        get
        {
            return instance.uIManager;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
