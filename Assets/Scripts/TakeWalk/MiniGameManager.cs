using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager instance;
    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    private int score = 0; // 게임 점수

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        // 게임 오버 상태에서 좌클릭을 누르면 현재 활성화된 씬을 재 로딩하여 재시작

        if (isGameover && Input.GetMouseButtonDown(0))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (isGameover)
        {
            score += newScore;
            //score.text = "Score" + score;
        }
    }
    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}