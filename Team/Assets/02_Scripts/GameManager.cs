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

    private void Update()
    {
        //Ÿ�̸�
        if (isGameStart && (!isGameOver || isClear))
        {
            float dwTime = 0;
            dwTime += Time.deltaTime;

            timer -= dwTime;
            if (timer <= 0)
            {
                isGameOver = true;
                Debug.Log("��ź�� �����Ͽ����ϴ�!!!");
                return;
            }
        }
    }

    void Start()
    {
        isGameOver = false;
        isGameStart = false;
    }

    //���̵� ���� �⺻�� ����
    public void SelectDifficulty()
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
        ShowResultUI();
    }

    public void GameClear()
    {
        isGameStart = false;
        isClear = true;
    }

    public void ShowResultUI()
    {
        //���� ���â On
    }

}
