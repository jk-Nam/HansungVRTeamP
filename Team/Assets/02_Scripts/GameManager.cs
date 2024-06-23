using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy = 1,
        Hard = 2
    }

    public Difficulty difficulty;

    public static GameManager Instance;

    public GameObject bomb;


    public float timer = 300.0f;
    public int defuesedCnt = 0;
    public int incorrectCnt = 0;
    public int totalModuleCnt;

    public bool isGameStart = false;
    public bool isGameOver = false;
    public bool isClear = false;


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

    void Start()
    {
        SelectDifficulty();
        isGameOver = false;
        isGameStart = false;
    }


    private void Update()
    {
        //타이머
        if (isGameStart && (!isGameOver || !isClear))
        {
            float dwTime = 0;
            dwTime += Time.deltaTime;

            timer -= dwTime;
            
            if (timer <= 0)
            {
                GameOver();
                Debug.Log("폭탄이 폭발하였습니다!!!");
                return;
            }
        }
    }

    //난이도 설정 기본은 쉬움
    public void SelectDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                //totalModuleCnt = 2;
                totalModuleCnt = 1;
                break;
            case Difficulty.Hard:
                //totalModuleCnt = 3;
                totalModuleCnt = 1;
                break;
            default:
                totalModuleCnt = 2;
                break;
        }
    }

    public void GameStart()
    {
        AddressableManager.Instance.CreateBomb();
        isGameOver = false;
        isGameStart = true;
        StartCoroutine(ServerManager.Instance.Result());
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameStart = false;
        ShowResultUI();
    }

    public void GameClear()
    {
        isGameStart = false;
        isClear = true;
        
        StartCoroutine(ServerManager.Instance.UpdateDate());
    }

    public void ShowResultUI()
    {
        //게임 결과창 On

    }

}
