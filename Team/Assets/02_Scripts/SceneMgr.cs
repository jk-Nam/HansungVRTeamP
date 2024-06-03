using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    //변수
    public int sceneNum = 0; //씬 번호

    //오브젝트
    //public GameObject menuPan; //프리팹
    public GameObject menu_0; //통신기
    public GameObject menu_1; //결과 조회
    public GameObject menu_2; //언어설정
    public GameObject menu_3; //옵션메뉴
    public GameObject bombOBJ; //폭탄
    public GameObject manualBook; //매뉴얼
    public GameObject resultPan; //결과패널

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


    void Start()
    {
        sceneNum = 1; //처음 씬번호 넣기
        SceneSwitch(sceneNum); //해당 번호 씬 호출
    }

    public void 

    //씬 스위치
    public void SceneSwitch(int Rooms)
    {

        if (Rooms >= 5)
        {
            Rooms = 0; //5번씬 대신에 0번으로
        }

        switch (Rooms)
        {
            case 0:
                RoomReset(); //오브젝트를 치운다
                //Debug.Log(Rooms + "번 씬으로 변경 되었다.");
                break;
            case 1:
                RoomReset();
                MainScene(); //메인씬 배치
                Debug.Log(Rooms + "번 메인씬으로 변경 되었다.");
                break;
            case 2:
                RoomReset();
                DefuserScene(); //인게임 해체반
                Debug.Log(Rooms + "번 해체씬으로 변경 되었다.");
                break;
            case 3:
                RoomReset();
                ExpertsScene(); //인게임 분석반
                Debug.Log(Rooms + "번 분석씬으로 변경 되었다.");
                break;
            case 4:
                RoomReset();
                GameOver(); //게임오버씬 배치
                Debug.Log(Rooms + "번 게임오버 되었다.");
                break;
        }

        sceneNum = Rooms; //씬에 직접 들어온 경우 씬번호 반영
    }

    //오브젝트 치우기 0
    public void RoomReset()
    {
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


        //Debug.Log("백룸으로 모두 보냈다.");
    }

    //게임 메인씬 1
    public void MainScene()
    {

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
    public void DefuserScene()
    {

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
    public void ExpertsScene()
    {

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
    public void GameOver()
    {
        //게임 결과 표시
        resultPan.transform.position = menuPos_4.position;

        //Debug.Log("게임결과 호출 되었다.");
    }

    //씬 변경 테스트
    public void SceneChange()
    {
        sceneNum++;
        SceneSwitch(sceneNum);
    }

    //오브젝트 위치 초기화
    public void SceneReset()
    {
        SceneSwitch(sceneNum);
        Debug.Log(sceneNum + "번 씬을 재호출");
    }
}
