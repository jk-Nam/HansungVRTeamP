using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    //변수
    public int sceneNum = 0; //씬 번호

    //필수 오브젝트
    //public GameObject menuPan; //프리팹
    public GameObject menu_0; //통신기
    public GameObject menu_1; //결과 조회
    public GameObject menu_2; //언어설정
    public GameObject menu_3; //옵션메뉴
    public GameObject bombOBJ; //폭탄
    public GameObject manualBook; //매뉴얼
    public GameObject resultPan; //결과패널
    
    //ISDK_HandGrabInteraction 좌표이동 권한관련
    public GameObject grab_0; //통신기
    public GameObject grab_1; //결과 조회
    public GameObject grab_2; //언어설정
    public GameObject grab_3; //옵션메뉴
    public GameObject grab_bomb; //폭탄
    public GameObject grab_Book; //매뉴얼

    //위치 모음
    public Transform menuPos_0; //왼쪽 1번 자리
    public Transform menuPos_1; //왼쪽 2번 자리
    public Transform menuPos_2; //오른쪽 2번 자리
    public Transform menuPos_3; //오른쪽 1번 자리
    public Transform menuPos_4; //가운데 위치

    public Transform backPos_0; //왼쪽 1번 자리
    public Transform backPos_1; //왼쪽 2번 자리
    public Transform backPos_2; //오른쪽 2번 자리
    public Transform backPos_3; //오른쪽 1번 자리
    public Transform backPos_4; //패널보관 위치
    public Transform backPos_5; //매뉴얼 보관위치
    public Transform backPos_6; //폭탄 보관위치

    //옵션패널 교체용
    public bool isOptionPan = true; //옵션교체
    public GameObject optionPan; //옵션패널
    public GameObject langPan; //언어설정
    //게임패널 UI 교체
    public bool isGamePen = true; //true입장전 / false입장후
    public bool isonlyVoice = false; //true게임중 / false게임아님
    public GameObject gameSelect; //게임설정 선택
    public GameObject gameInPan; //역할 선택
    public GameObject onlyVoice; //온리보이스 문구

    void Start()
    {

        sceneNum = 1; //처음 씬번호 넣기
        SceneSwitch(sceneNum); //해당 번호 씬 호출
    }

    //게임채널 접속 상태 더미 함수
    public void RoomInOut()
    {
        //게임채널 입장 / 나가기
        isGamePen = !isGamePen;
        //외부 호출시 bool isGamePen 를 통해서 사용가능
        GmaePenBtn(); //패널 상태를 갱신한다
    }

    //씬변경 스위치
    public void SceneSwitch(int Rooms)
    {

        if (Rooms >= 6)
        {
            Rooms = 1; //없는 씬 대신에 메인씬으로
        }

        // 화면 페이드 인 또는 블라인드 예정

        RoomReset(); //오브젝트를 치운다

        switch (Rooms)
        {
            case 0:
                //로그인? 같은 메인씬보다 먼저 호출의 겨우
                //Debug.Log(Rooms + "번 씬으로 변경 되었다.");
                break;
            case 1:
                MainScene(); //메인씬 배치
                Debug.Log(Rooms + "번 메인씬으로 변경 되었다.");
                break;
            case 2:
                DefuserScene(); //인게임 해체반
                Debug.Log(Rooms + "번 해체씬으로 변경 되었다.");
                break;
            case 3:
                ExpertsScene(); //인게임 분석반
                Debug.Log(Rooms + "번 분석씬으로 변경 되었다.");
                break;
            case 4:
                GameOver(); //게임오버씬 배치
                Debug.Log(Rooms + "번 게임오버 되었다.");
                break;
            case 5:
                GameRetry(); //게임 패널 소환
                Debug.Log(Rooms + "번 게임 재도전 연출");
                break;
        }

        //화면 블라인드 제거 또는 페이드 아웃 예정

        sceneNum = Rooms; //씬에 직접 들어온 경우 씬번호 반영
    }

    //오브젝트 치우기 0
    void RoomReset()
    {
        // 핸드그랩을 비활성 하여, 포지션을 풀어준다.
        grab_0.SetActive(false);
        grab_1.SetActive(false);
        grab_2.SetActive(false);
        grab_3.SetActive(false);
        grab_bomb.SetActive(false);
        grab_Book.SetActive(false);

        // 모든 오브젝트를 벡룸에 정리한다.
        menu_0.transform.position = backPos_0.position;
        menu_0.transform.rotation = backPos_0.rotation;

        menu_1.transform.position = backPos_1.position;
        menu_1.transform.rotation = backPos_1.rotation;

        menu_2.transform.position = backPos_2.position;
        menu_2.transform.rotation = backPos_2.rotation;

        menu_3.transform.position = backPos_3.position;
        menu_3.transform.rotation = backPos_3.rotation;

        resultPan.transform.position = backPos_4.position;
        resultPan.transform.rotation = backPos_4.rotation;

        bombOBJ.transform.position = backPos_5.position;
        bombOBJ.transform.rotation = backPos_5.rotation;

        manualBook.transform.position = backPos_6.position;
        manualBook.transform.rotation = backPos_6.rotation;

        // 핸드그랩을 활성화 해준다.
        grab_0.SetActive(true);
        grab_1.SetActive(true);
        grab_2.SetActive(true);
        grab_3.SetActive(true);
        grab_bomb.SetActive(true);
        grab_Book.SetActive(true);

        //Debug.Log("백룸으로 모두 보냈다.");
    }

    //게임 메인씬 1
    void MainScene()
    {
        //메인화면은 게임채널 접속전 상태이다.
        isGamePen = true; //게임채널 접속전
        isonlyVoice = false;
        GmaePenBtn(); //게임패널을 초기화 시킨다

        //메뉴판 4개의 위치를 옮긴다.
        menu_0.transform.position = menuPos_0.position;
        menu_0.transform.rotation = menuPos_0.rotation;

        menu_1.transform.position = menuPos_1.position;
        menu_1.transform.rotation = menuPos_1.rotation;

        menu_2.transform.position = menuPos_2.position;
        menu_2.transform.rotation = menuPos_2.rotation;

        menu_3.transform.position = menuPos_3.position;
        menu_3.transform.rotation = menuPos_3.rotation;

        //Debug.Log("메인씬 호출 되었다.");
    }

    //인 게임 씬 (해체반) 2
    void DefuserScene()
    {
        //게임중 통신기
        isonlyVoice = true;
        GmaePenBtn(); //온리보이스

        //필요한 OBJ 기본위치
        menu_0.transform.position = menuPos_0.position;
        menu_0.transform.rotation = menuPos_0.rotation;

        menu_3.transform.position = menuPos_3.position;
        menu_3.transform.rotation = menuPos_3.rotation;

        bombOBJ.transform.position = menuPos_4.position;
        bombOBJ.transform.rotation = menuPos_4.rotation;

        //Debug.Log("해체씬 호출 되었다.");
    }

    //인 게임 씬 (분석반) 3
    void ExpertsScene()
    {
        //게임중 통신기
        isonlyVoice = true;
        GmaePenBtn(); //온리보이스

        //필요한 OBJ 기본위치
        menu_0.transform.position = menuPos_0.position;
        menu_0.transform.rotation = menuPos_0.rotation;

        menu_3.transform.position = menuPos_3.position;
        menu_3.transform.rotation = menuPos_3.rotation;

        manualBook.transform.position = menuPos_4.position;
        manualBook.transform.rotation = menuPos_4.rotation;

        //Debug.Log("분석씬 호출 되었다.");
    }

    //게임 오버 연출 4
    void GameOver()
    {
        //게임 결과 표시
        resultPan.transform.position = menuPos_4.position;
        //isGamePen = false; //재도전 준비
        isonlyVoice = false;
        GmaePenBtn(); //온리보이스 해제

        //Debug.Log("게임결과 호출 되었다.");
    }

    //게임 재도전 5
    void GameRetry()
    {
        //게임 매뉴 소환
        menu_0.transform.position = menuPos_4.position;

        menu_3.transform.position = menuPos_3.position;
        menu_3.transform.rotation = menuPos_3.rotation;
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
        if (isonlyVoice)
        {
            //isonlyVoice가 들어오면 게임중! 작동불가!
            gameSelect.SetActive(false);
            gameInPan.SetActive(false);
            onlyVoice.SetActive(true);
        }else if(isGamePen)
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
