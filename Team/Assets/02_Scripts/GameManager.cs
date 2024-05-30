using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Hard
    }

    public Difficulty difficulty;

    public static GameManager Instance;

    public GameObject bomb;


    public float timer = 300.0f;
    public int defuesedCnt = 0;
    public int incorrectCnt = 0;

    public bool isGameStart = false;
    public bool isGameOver = false;
    public bool isClear = false;

    int moduleCnt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //타이머
        if (isGameStart && !isGameOver)
        {
            float dwTime = 0;
            dwTime += Time.deltaTime;

            timer -= dwTime;
            if (timer <= 0)
            {
                isGameOver = true;
                return;
            }
        }
    }

    void Start()
    {
        isGameOver = false;
        isGameStart = false;
    }

    //난이도 설정 기본은 쉬움
    public void SelectDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                moduleCnt = 2;
                break;
            case Difficulty.Hard:
                moduleCnt = 3;
                break;
            default:
                moduleCnt = 2;
                break;
        }
    }

    public void GameStart()
    {
        isGameOver = false;
        isGameStart = true;
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameStart = false;
        ShowResultUI();
    }

    public void Clear()
    {

    }

    public void ShowResultUI()
    {
        //게임 결과창 On
    }

}
