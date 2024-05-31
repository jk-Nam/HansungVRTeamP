using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public GameObject menuPan; //프리팹
    public GameObject menu_0; //통신기
    public GameObject menu_1; //결과 조회
    public GameObject menu_2; //언어설정
    public GameObject menu_3; //옵션메뉴
    public GameObject menu_4; //폭탄
    public GameObject menu_5; //매뉴얼

    public Transform menuPos_0; //왼쪽 1번 자리
    public Transform menuPos_1; //왼쪽 2번 자리
    public Transform menuPos_2; //오른쪽 2번 자리
    public Transform menuPos_3; //오른쪽 1번 자리
    public Transform menuPos_4; //가운데 위치

    void Start()
    {
        //메뉴 오브젝트의 외형을 정한다.
        menu_0 = menuPan;
        menu_1 = menuPan;
        menu_2 = menuPan;
        menu_3 = menuPan;

        MainScene();
    }

    //책상만 남기고 모두 제거
    public void CleanView()
    {
        Destroy(menu_0);
        Destroy(menu_1);    
        Destroy(menu_2);
        Destroy(menu_3);
        Destroy(menu_4);
        Destroy(menu_5);
    }

    //게임 메인씬
    public void MainScene()
    {
        //CleanView(); 

        Instantiate(menu_0, menuPos_0.position, menuPos_0.rotation);
        Instantiate(menu_1, menuPos_1.position, menuPos_1.rotation);
        Instantiate(menu_2, menuPos_2.position, menuPos_2.rotation);
        Instantiate(menu_3, menuPos_3.position, menuPos_3.rotation);
    }

    //인 게임 씬 (해체반)
    //인 게임 씬 (분석반)
    //게임 결과 & 기록조회
}
