using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TakeWalkScene : UI_Scene
{
    GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set 
        {
            _player = value;
            _playerZPos = _player.transform.position.z;
        }
    }

    float _playerZPos;

    int _bestScore;
    int _currentScore = 0;

    enum TextMeshProsUGUIs
    {
        CountdownText,
        BestScoreText,
        CurrentScoreText,
    }

    enum Images
    {
        DarkBackground,
        Blocker,
        ClickHereToMoveImage
    }

    enum Buttons
    {
        RetryButton,
        GoBackButton,
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveUIObjectByEnum<TextMeshProUGUI>(typeof(TextMeshProsUGUIs));
        SaveUIObjectByEnum<Image>(typeof(Images));
        SaveUIObjectByEnum<Button>(typeof(Buttons));

        StartCoroutine(Countdown());

        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.RetryButton).gameObject, Defines.UIEventType.PointDown, RetryGame);
        BindFuntionToHandler(GetUIObject<Button>((int)Buttons.GoBackButton).gameObject, Defines.UIEventType.PointDown, BackToHome);
        HideGameOverUI();
        LoadBestScore();
        UpdateScoreUI();

        Player.GetComponent<TakeWalkPuppyController>().OnGameOverAction -= ShowGameOverUI;
        Player.GetComponent<TakeWalkPuppyController>().OnGameOverAction += ShowGameOverUI;
    }
    IEnumerator Countdown()
    {
        int interval = Managers.DataManager.TakeWalkStartInterval;
        for (int i = interval; i >= 0; i--)
        {
            GetUIObject<TextMeshProUGUI>((int)TextMeshProsUGUIs.CountdownText).text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        GetUIObject<TextMeshProUGUI>((int)TextMeshProsUGUIs.CountdownText).text = "";
        GetUIObject<Image>((int)Images.Blocker).gameObject.SetActive(false);
        GetUIObject<Image>((int)Images.ClickHereToMoveImage).gameObject.SetActive(false);
    }

    void RetryGame(PointerEventData data)
    {
        HideGameOverUI();
        Managers.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void BackToHome(PointerEventData data)
    {
        Managers.SceneManager.LoadScene("HouseScene");
        
    }
    void ShowGameOverUI()
    {
        
        DecreaseStressIfSuccess();
        GetUIObject<Image>((int)Images.DarkBackground).gameObject.SetActive(true);
        GetUIObject<Button>((int)Buttons.RetryButton).gameObject.SetActive(true);
        GetUIObject<Button>((int)Buttons.GoBackButton).gameObject.SetActive(true);

        if (_bestScore < _currentScore)
        {
            _bestScore = _currentScore;
            SaveBestScore();
        }
    }

    public void HideGameOverUI()
    {
        GetUIObject<Image>((int)Images.DarkBackground).gameObject.SetActive(false);
        GetUIObject<Button>((int)Buttons.RetryButton).gameObject.SetActive(false);
        GetUIObject<Button>((int)Buttons.GoBackButton).gameObject.SetActive(false);
    }

    public void UpdateScoreUI()
    {
        GetUIObject<TextMeshProUGUI>((int)TextMeshProsUGUIs.BestScoreText).text = "Best Score: " + _bestScore + " m";
        GetUIObject<TextMeshProUGUI>((int)TextMeshProsUGUIs.CurrentScoreText).text = "Current Score: " + _currentScore + " m";
    }

    void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
    }

    private void Update()
    {
        UpdateScore();
        UpdateScoreUI();
    }

    void UpdateScore()
    {
        _currentScore = (int)((Player.transform.position.z - _playerZPos) * 10);
    }

    void DecreaseStressIfSuccess()
    {
        //HideGameOverUI();
        if (_currentScore >= 3000.0f)
        {
            Managers.UIManager.ShowPopupUI<UI_WalkSuccess>();
            Debug.Log("산책 성공");
            Managers.DataManager.Stress = Managers.DataManager.Stress / 2;
        }
    }
}
