using Photon.Pun;
using Photon.Realtime;
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
    public Transform bombPos;


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
        //타이머
        if (isGameStart && (!isGameOver || isClear))
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

    void Start()
    {
        SelectDifficulty();
        isGameOver = false;
        isGameStart = false;
    }

    //난이도 설정 기본은 쉬움
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
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            // 두 명의 플레이어가 있는지 확인
            Player[] players = PhotonNetwork.PlayerList;
            string player1ID = players[0].UserId;
            string player2ID = players[1].UserId;
        }
        else
        {
            Debug.LogError("방에 두 명의 플레이어가 없습니다.");
        }
        StartCoroutine(ServerManager.Instance.Result());
        isGameOver = false;
        isGameStart = true;
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameStart = false;
        StartCoroutine(ServerManager.Instance.UpdateData());
        ShowResultUI();
    }

    public void GameClear()
    {
        isGameStart = false;
        isClear = true;
        StartCoroutine(ServerManager.Instance.UpdateData());
    }

    public void ShowResultUI()
    {
        //게임 결과창 On
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
