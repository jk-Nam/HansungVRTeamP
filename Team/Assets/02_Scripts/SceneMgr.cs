using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    public int sceneNum = 1; // 씬 번호

    public GameObject[] menus = new GameObject[4]; // 메뉴들
    public GameObject bombOBJ; // 폭탄
    public GameObject manualBook; // 매뉴얼
    public GameObject resultPan; // 결과패널

    private GameObject[] grabs = new GameObject[6]; // 좌표이동 권한

    // 위치 모음
    public Transform[] menuPositions = new Transform[5]; // 메뉴 위치
    public Transform[] backPositions = new Transform[7]; // 보관 위치

    //옵션패널 교체용
    public bool isOptionPan = true; //옵션교체
    public GameObject optionPan; //옵션패널
    public GameObject langPan; //언어설정

    //게임패널 UI 교체
    public bool isGamePen = true; //true입장전 / false입장후
    public bool isOnlyVoice = false; //true게임중 / false게임아님
    public GameObject gameSelect; //게임설정 선택
    public GameObject gameInPan; //역할 선택
    public GameObject onlyVoice; //온리보이스 문구

    void Awake()
    {
        OBJSetting();//오브젝트 자동지정

        HandGrabSetting();//ISDK_HandGrabInteraction 찾아주기
        //사운드 매니저 연결
    }

    void Start()
    {
        //sceneNum = 1; // 처음 씬번호 넣기
        SceneSwitch(sceneNum); // 해당 번호 씬 호출
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
        if (menus[1] == null) menus[1] = GameObject.Find("Menu_1(GameResult)");
        if (menus[2] == null) menus[2] = GameObject.Find("Menu_2(LanguageQuick)");
        if (menus[3] == null) menus[3] = GameObject.Find("Menu_3(Option)");


        if (bombOBJ == null) bombOBJ = GameObject.Find("BombOBJ(Dummy)");
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
                SoundMgr.instance.PlayBGM(0); //BGM인덱스번호 변경
                break;
            case 2:
                DefuserScene(); // 인게임 해체반
                Debug.Log(Rooms + "번 해체씬으로 변경 되었다.");
                SoundMgr.instance.PlayBGM(0);
                break;
            case 3:
                ExpertsScene(); // 인게임 분석반
                Debug.Log(Rooms + "번 분석씬으로 변경 되었다.");
                SoundMgr.instance.PlayBGM(0);
                break;
            case 4:
                GameOver(); // 게임오버씬 배치
                Debug.Log(Rooms + "번 게임오버 되었다.");
                SoundMgr.instance.PlayBGM(0);
                break;
            case 5:
                GameRetry(); //게임 패널 소환
                Debug.Log(Rooms + "번 게임 재도전 연출");
                SoundMgr.instance.PlayBGM(0);
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

        resultPan.transform.position = backPositions[4].position;
        resultPan.transform.rotation = backPositions[4].rotation;

        bombOBJ.transform.position = backPositions[5].position;
        bombOBJ.transform.rotation = backPositions[5].rotation;

        manualBook.transform.position = backPositions[6].position;
        manualBook.transform.rotation = backPositions[6].rotation;

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

        menus[3].transform.position = menuPositions[3].position;
        menus[3].transform.rotation = menuPositions[3].rotation;

        bombOBJ.transform.position = menuPositions[4].position;
        bombOBJ.transform.rotation = menuPositions[4].rotation;
    }

    void ExpertsScene()
    {
        //게임중 통신기
        isOnlyVoice = true;
        GmaePenBtn(); //온리보이스

        menus[0].transform.position = menuPositions[0].position;
        menus[0].transform.rotation = menuPositions[0].rotation;

        menus[3].transform.position = menuPositions[3].position;
        menus[3].transform.rotation = menuPositions[3].rotation;

        manualBook.transform.position = menuPositions[4].position;
        manualBook.transform.rotation = menuPositions[4].rotation;
    }

    void GameOver()
    {
        resultPan.transform.position = menuPositions[4].position;

        isOnlyVoice = false;
        GmaePenBtn(); //온리보이스 해제
    }

    void GameRetry()
    {
        menus[0].transform.position = menuPositions[4].position;

        menus[3].transform.position = menuPositions[3].position;
        menus[3].transform.rotation = menuPositions[3].rotation;
    }

    private void HandGrabSetting()
    {
        grabs[0] = menus[0].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[1] = menus[1].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[2] = menus[2].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[3] = menus[3].transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[4] = bombOBJ.transform.Find("ISDK_HandGrabInteraction").gameObject;
        grabs[5] = manualBook.transform.Find("ISDK_HandGrabInteraction").gameObject;
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
            gameSelect.SetActive(false);
            gameInPan.SetActive(false);
            onlyVoice.SetActive(true);
        }
        else if (isGamePen)
        {
            //게임채널 접속 화면
            onlyVoice.SetActive(false);
            gameSelect.SetActive(true);
            gameInPan.SetActive(false);
        }
        else
        {
            //게임채널 접속 후
            onlyVoice.SetActive(false);
            gameSelect.SetActive(false);
            gameInPan.SetActive(true);
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
}
