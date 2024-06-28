
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeModule : BombModule
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject Goal;

    public float moveAmount = 0.1f; // 이동량

    public GameObject mainLight;

    // UI 버튼을 연결할 변수
    public UnityEngine.UI.Button upButton;
    public UnityEngine.UI.Button downButton;
    public UnityEngine.UI.Button leftButton;
    public UnityEngine.UI.Button rightButton;
    
    private Vector3 initialPosition;

    Bomb bomb;

    private GameObject bombObject;
    private float bombObjectXScale;

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

        // "BOMB" 태그를 가진 오브젝트를 찾습니다
        bombObject = GameObject.FindWithTag("BOMB");

        // 오브젝트가 존재하는지 확인합니다
        if (bombObject != null)
        {
            // 오브젝트의 x축 스케일 값을 변수에 설정합니다
            bombObjectXScale = bombObject.transform.localScale.x;
            moveAmount = (float)(bombObjectXScale * 0.1);

            Debug.Log("BOMB 오브젝트의 x축 스케일 값: " + bombObjectXScale);
        }
        else
        {
            Debug.LogWarning("BOMB 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
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
            SoundMgr.instance.PlaySFX(26);
            player.transform.localPosition = initialPosition;
            
            //Fail();
            GameManager.Instance.incorrectCnt++;
            if (GameManager.Instance.incorrectCnt >= 3)
            {
                upButton.gameObject.SetActive(false);
                downButton.gameObject.SetActive(false);
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(false);
                player.gameObject.SetActive(false);
                bomb.Fail();
                Debug.Log("Game Over!!!");
            }



        }
        else if (other.gameObject.CompareTag("MazeGoal"))
        {
            Debug.Log("클리어!");
            SoundMgr.instance.PlaySFX(14);
            upButton.gameObject.SetActive(false);
            downButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            mainLight.GetComponent<Image>().color = Color.green;

            DefuseModule();
            
        }



    }
}