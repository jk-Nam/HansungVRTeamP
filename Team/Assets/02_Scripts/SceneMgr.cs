using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    public int sceneNum = 1; // 씬 번호

    public GameObject[] menus = new GameObject[2]; // 메뉴들
    public GameObject bombOBJ; // 폭탄
    public GameObject manualBook; // 매뉴얼
    public GameObject resultPan; // 결과패널

    private GameObject[] grabs = new GameObject[3]; // 좌표이동 권한

    // 위치 모음
    public Transform[] menuPositions = new Transform[3]; // 메뉴 위치
    public Transform[] backPositions = new Transform[5]; // 보관 위치

    //옵션패널 교체용
    public bool isOptionPan = true; //옵션교체
    public GameObject optionPan; //옵션패널
    public GameObject langPan; //언어설정

    //게임패널 UI 교체
    public bool isGamePen = true; //true입장전 / false입장후
    public bool isOnlyVoice = false; //true게임중 / false게임아님
    public bool isPhot = false; //포톤보이스 포함인가?
    public GameObject gameSelect; //게임설정 선택
    public GameObject gameInPan; //역할 선택
    public GameObject onlyVoice; //온리보이스 문구
    public GameObject gameoverPan; //게임오버패널
    
    void Awake()
    {
        OBJSetting();//오브젝트 자동지정

        HandGrabSetting();//ISDK_HandGrabInteraction 찾아주기
    }

    void Start()
    {
        SceneSwitch(sceneNum); // 해당 번호 씬 호출
    }

    void Update()
    {
        if (GameManager.Instance.isClear || GameManager.Instance.isGameOver) 
        {
            //폭탄이 종료되면
            StartCoroutine("BombEnd");
        }
    }

    IEnumerator BombEnd()
    {
        GameManager.Instance.isClear = false;
        GameManager.Instance.isGameOver = false;

        GameObject bombGrab = bombOBJ.transform.Find("ISDK_HandGrabInteraction").gameObject;
        bombGrab.SetActive(false);
        yield return new WaitForSecondsRealtime(1.0f);
        
        SceneSwitch(4);
    }

    //게임채널 접속 상태 더미 함수
    public void RoomInOut()
    {
        //게임채널 입장 / 나가기
        isGamePen = !isGamePen;
        GmaePenBtn(); //패널 상태를 갱신한다
    }



    private void OBJSetting()
    {
        if (menus[0] == null) menus[0] = GameObject.Find("Menu_0(GameConnect)");
        if (menus[1] == null) menus[1] = GameObject.Find("Menu_3(Option)");


        if (manualBook == null) manualBook = GameObject.Find("ManualBook");
        if (resultPan == null) resultPan = GameObject.Find("ResultPanel");


        for (int i = 0; i < menuPositions.Length; i++)
        {
            if (menuPositions[i] == null) menuPositions[i] = GameObject.Find("MenuPos_" + i).transform;
        }

        for (int i = 0; i < backPositions.Length; i++)
        {
            if (backPositions[i] == null) backPositions[i] = GameObject.Find("BackPos_" + i).transform;
        }

        if (optionPan == null) optionPan = GameObject.Find("OptionPan");
        if (langPan == null) langPan = GameObject.Find("LangPan");

        if (gameSelect == null) gameSelect = GameObject.Find("GameSelect");
        if (gameInPan == null) gameInPan = GameObject.Find("GameInPan");
        if (onlyVoice == null) onlyVoice = GameObject.Find("OnlyVoicePanel");
    }

    public void PhotonPanel()
    {
        isPhot = true;
    }

    public void SceneSwitch(int Rooms)
    {
        if (Rooms >= 6)
        {
            Rooms = 1; // 없는 씬 대신에 메인씬으로
        }

        RoomReset(); // 오브젝트를 치운다

        switch (Rooms)
        {
            case 0:
                break;
            case 1:
                MainScene(); // 메인씬 배치
                Debug.Log(Rooms + "번 메인씬으로 변경 되었다.");
                //SoundMgr.instance.PlayBGM(0); //BGM인덱스번호 변경
                break;
            case 2:
                DefuserScene(); // 인게임 해체반
                Debug.Log(Rooms + "번 해체씬으로 변경 되었다.");
                break;
            case 3:
                ExpertsScene(); // 인게임 분석반
                Debug.Log(Rooms + "번 분석씬으로 변경 되었다.");
                break;
            case 4:
                GameOver(); // 게임오버씬 배치
                Debug.Log(Rooms + "번 게임오버 되었다.");
                break;
            case 5:
                GameRetry(); //게임 패널 소환
                Debug.Log(Rooms + "번 게임 재도전 연출");
                break;
        }
        sceneNum = Rooms; //씬에 직접 들어온 경우 씬번호 반영
    }

    void RoomReset()
    {
        foreach (GameObject grab in grabs)
        {
            grab.SetActive(false);
        }

        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].transform.position = backPositions[i].position;
            menus[i].transform.rotation = backPositions[i].rotation;
        }

        resultPan.transform.position = backPositions[2].position;
        resultPan.transform.rotation = backPositions[2].rotation;

        manualBook.transform.position = backPositions[3].position;
        manualBook.transform.rotation = backPositions[3].rotation;

        foreach (GameObject grab in grabs)
        {
            grab.SetActive(true);
        }
    }

    void MainScene()
    {
        //메인화면은 게임채널 접속전 상태이다.
        isGamePen = true; //게임채널 접속전
        isOnlyVoice = false;
        GmaePenBtn(); //게임패널을 초기화 시킨다

        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].transform.position = menuPositions[i].position;
            menus[i].transform.rotation = menuPositions[i].rotation;
        }
    }

    void DefuserScene()
    {
        //게임중 통신기
        isOnlyVoice = true;
        GmaePenBtn(); //온리보이스

        menus[0].transform.position = menuPositions[0].position;
        menus[0].transform.rotation = menuPositions[0].rotation;

        menus[1].transform.position = menuPositions[1].position;
        menus[1].transform.rotation = menuPositions[1].rotation;

        if (bombOBJ == null) bombOBJ = GameObject.Find("BombReal(Clone)");

        GameObject bombGrab = bombOBJ.transform.Find("ISDK_HandGrabInteraction").gameObject;
        bombGrab.SetActive(false);
        bombOBJ.transform.position = menuPositions[2].position;
        bombOBJ.transform.rotation = menuPositions[2].rotation;
        bombGrab.SetActive(true);

        StartCoroutine("BombStart");
    }

    IEnumerator BombStart()
    {
        //게임 누적데이터 초기화
        GameManager.Instance.timer = 300.0f;
        GameManager.Instance.defuesedCnt = 0;
        GameManager.Instance.incorrectCnt = 0;

        //게임매니저의 시작함수 호출
        yield return new WaitForSecondsRealtime(1.0f);
        GameManager.Instance.GameStart();
    }

    void ExpertsScene()
    {
        //게임중 통신기
        isOnlyVoice = true;
        GmaePenBtn(); //온리보이스

        menus[0].transform.position = menuPositions[0].position;
        menus[0].transform.rotation = menuPositions[0].rotation;

        menus[1].transform.position = menuPositions[1].position;
        menus[1].transform.rotation = menuPositions[1].rotation;

        manualBook.transform.position = menuPositions[2].position;
        manualBook.transform.rotation = menuPositions[2].rotation;
    }

    void GameOver()
    {
        //게임오버 패널가리기
        gameoverPan.SetActive(false);

        resultPan.transform.position = menuPositions[2].position;

        isOnlyVoice = false;
        GmaePenBtn(); //온리보이스 해제

        StopCoroutine("BombStart");
        StopCoroutine("BombEnd");
        DestroyBomb(); //폭탄 지우기
    }

    void GameRetry()
    {
        menus[0].transform.position = menuPositions[2].position;

        menus[1].transform.position = menuPositions[1].position;
        menus[1].transform.rotation = menuPositions[1].rotation;
    }

    private void HandGrabSetting()
    {
        grabs[0] = menus[0].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[1] = menus[1].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[2] = manualBook.transform.Find("ISDK_HandGrabInteraction").gameObject;

    }

    // 옵션메뉴 패널변경 
    public void LanguageBtn()
    {
        //옵션 Language 누르면 오브젝트가 교체
        isOptionPan = !isOptionPan;

        if (isOptionPan)
        {
            optionPan.SetActive(true);
            langPan.SetActive(false);
        }
        else
        {
            optionPan.SetActive(false);
            langPan.SetActive(true);
        }
    }

    // 게임메뉴 패널변경 갱신
    public void GmaePenBtn()
    {
        //채널입장 또는 게임중 패널상태를 구분함
        if (isOnlyVoice)
        {
            //isonlyVoice가 들어오면 게임중! 작동불가!
            gameInPan.SetActive(false);
            onlyVoice.SetActive(true);
            if (!isPhot) { gameSelect.SetActive(false); }
        }
        else if (isGamePen)
        {
            //게임채널 접속 화면
            onlyVoice.SetActive(false);
            gameInPan.SetActive(false);
            if (!isPhot) { gameSelect.SetActive(true); }
        }
        else
        {
            //게임채널 접속 후
            onlyVoice.SetActive(false);
            gameInPan.SetActive(true);
            if (!isPhot) { gameSelect.SetActive(false); }
        }

    }

    //해당 씬 오브젝트 위치 초기화
    public void SceneReset()
    {
        SceneSwitch(sceneNum);
        Debug.Log(sceneNum + "번 씬을 재호출");
    }

    //씬 변경 테스트 용
    public void SceneChange()
    {
        sceneNum++;
        SceneSwitch(sceneNum);
    }

    //폭탄 지우기
    public void DestroyBomb()
    {
        if (bombOBJ == null)
        {
            //폭탄이 없으면 넘어간다.
        }
        else
        {
            //생성된 폭탄을 제거한다.
            Destroy(bombOBJ);
        }
        
    }
}
