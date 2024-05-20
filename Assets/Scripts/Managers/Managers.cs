using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance
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

    InputManager _inputManager = new InputManager();
    public static InputManager InputManager
    {
        get
        {
            return Instance._inputManager;
        }
    }

    UIManager _uIManager = new UIManager();
    public static UIManager UIManager
    {
        get
        {
            return Instance._uIManager;
        }
    }

    SceneManager _sceneManager = new SceneManager();
    public static SceneManager SceneManager
    {
        get
        {
            return Instance._sceneManager;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 클릭 처리
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (InputManager.PointerDownInputAction != null)
                {
                    InputManager.PointerDownInputAction.Invoke(Input.mousePosition);
                }
            }
        }

        // 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!IsPointerOverUIObject(touch))
                {
                    InputManager.PointerDownInputAction.Invoke(touch.position);
                }
            }
        }
    }

    private bool IsPointerOverUIObject(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(touch.position.x, touch.position.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
