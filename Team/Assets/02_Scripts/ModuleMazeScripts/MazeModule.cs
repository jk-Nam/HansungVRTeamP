
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeModule : BombModule
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject Goal;

    public float moveAmount = 0.1f; // 이동량

    // UI 버튼을 연결할 변수
    public UnityEngine.UI.Button upButton;
    public UnityEngine.UI.Button downButton;
    public UnityEngine.UI.Button leftButton;
    public UnityEngine.UI.Button rightButton;
    
    private Vector3 initialPosition;

    Bomb bomb;

    private void Awake()
    {
       // bomb = GameObject.FindGameObjectWithTag("BOMB").GetComponent<Bomb>();
    }

    void Start()
    {
        // 플레이어의 초기 위치 저장 (로컬로 저장해야 함.)
        initialPosition = player.transform.localPosition;

        // UI 버튼 클릭 이벤트 리스너 추가
        upButton.onClick.AddListener(MoveUp);
        downButton.onClick.AddListener(MoveDown);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);

        InitiallizeModule();
    }

    public override void InitiallizeModule()
    {
        // 모듈 초기화 코드 (필요에 따라 추가)
        moduleType = BombMoudleType.Maze;
    }

    public override void DefuseModule()
    {
        isDefused = true;
        GameManager.Instance.defuesedCnt++;
        if (GameManager.Instance.defuesedCnt == GameManager.Instance.totalModuleCnt)
        {
            GameManager.Instance.GameClear();
        }
        // 모듈 해제 시 필요한 추가 동작
    }

    //public override void Fail()
    //{
    //   // incorrectCnt++;
    //    // 실패 시 필요한 추가 동작
    //}

    //// 버튼 입력에 따른 이동 함수
    void MoveUp()
    {
        player.transform.Translate(Vector3.up * moveAmount);
    }

    void MoveDown()
    {
        player.transform.Translate(Vector3.down * moveAmount);
    }

    void MoveLeft()
    {
        player.transform.Translate(Vector3.left * moveAmount);
    }

    void MoveRight()
    {

        player.transform.Translate(Vector3.right * moveAmount);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeWall"))
        {
            // 플레이어의 위치를 초기 위치로 리셋
            Debug.Log("실패");
            player.transform.localPosition = initialPosition;
            //Fail();
            GameManager.Instance.incorrectCnt++;
            if (GameManager.Instance.incorrectCnt >= 3)
            {
                bomb.Fail();
                Debug.Log("Game Over!!!");
            }



        }
        else if (other.gameObject.CompareTag("MazeGoal"))
        {
            Debug.Log("클리어!");
            DefuseModule();
        }



    }
}