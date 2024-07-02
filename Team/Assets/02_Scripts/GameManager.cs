using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public GameObject gameOverPanle;
    public Transform bombPos;


    public float timer = 300.0f;
    public int defuesedCnt = 0;
    public int incorrectCnt = 0;
    public int totalModuleCnt;


    public bool isGameStart = false;
    public bool isGameOver = false;
    public bool isClear = false;

    private NetworkManager networkManager;

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
        //SelectDifficulty();
        // NetworkManager 인스턴스 참조
        networkManager = NetworkManager.Instance;

    }

    private void Update()
    {
        //타이머
        if (isGameStart && (!isGameOver || isClear))
        {
            float dwTime = 0;
            dwTime += Time.deltaTime;

            timer -= dwTime;
            if (timer <= 0)
            {
                isGameOver = true;
                Debug.Log("폭탄이 폭발하였습니다!!!");
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
    public void SelectDifficulty(int i)
    {

        switch (difficulty)
        {
            case Difficulty.Easy:
                totalModuleCnt = 2;
                break;
            case Difficulty.Hard:
                totalModuleCnt = 3;
                break;
            default:
                totalModuleCnt = 2;
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
        NetworkManager.Instance.TriggerGameOver();
    }

    public void GameClear()
    {
        isGameStart = false;
        isClear = true;
    }

    public void ShowResultUI()
    {
        if (gameOverPanle != null)
        {
            gameOverPanle.SetActive(true);
        }
    }

    public void CreateBomb()
    {
        if (!isGameStart && !isGameOver)
        {
            GameObject bombPrefab = Instantiate(bomb);

            if (bombPrefab != null)
            {
                bombPrefab.transform.position = bombPos.transform.position;
                Debug.Log("폭탄 생성 완료: " + bomb.name);
            }
            else
            {
                Debug.LogError("폭탄 생성 실패: 로드된 GameObject가 null입니다.");
            }
        }
    }
}
