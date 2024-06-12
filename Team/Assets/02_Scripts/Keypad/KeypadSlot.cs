using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeypadSlot : MonoBehaviour
{
    public ItemBuffer itemBuffer; //아이템 리스트
    public GameObject keyRoot; //심볼이 들어갈 장소
    private List<Slot> keySlot; //메뉴얼 심볼 리스트
    public int selectLine; //선택된 심볼들
    private List<int> truekey; //정답 리스트
    private List<int> truekey2; //작성용 리스트
    private List<int> falsekey; //문제용 리스트

    public Image mainLight; //메인 신호등

    void Start()
    {
        itemBuffer = GameObject.Find("KeypadScript").GetComponent<ItemBuffer>();

        //6줄 중에서 랜덤으로 1줄 선택 한다
        selectLine = Random.Range(0, 6);

        //keyRoot = new List<Slot>(); //슬롯 리스트 정의

        //넣어야 할 문양번호 리스트
        List<int> numbers = new List<int>()
        {   28, 13, 30, 12, 7, 9, 23,
            16, 28, 23, 26, 3, 9, 20,
            1, 8, 26, 5, 15, 30, 3,
            11, 21, 31, 7, 5, 20, 4,
            24, 4, 31, 22, 21, 19, 2,
            11, 16, 27, 14, 24, 18, 6
        };

        //선택한 줄을 리스트로 만든다.
        truekey = new List<int>();
        truekey2 = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            truekey.Add(numbers[i + (7 * selectLine)]);
            truekey2.Add(numbers[i + (7 * selectLine)]);
        }
        //선택한 줄에서 4개로 정답을 만든다.
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 7 - i);
            truekey.RemoveAt(j);
            truekey2.RemoveAt(j); //정답리스트 2개 작성
        }
        //문제용 4개를 임의로 섞는다. 
        falsekey = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, 4 - i);
            falsekey.Add(truekey2[j]); //키패드버튼
            truekey2.RemoveAt(j); //사용한 심볼은 제거
        }
        
        keySlot = new List<Slot>(); //슬롯 리스트 정의
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            var slot = keyRoot.transform.GetChild(i).GetComponent<Slot>();
            int j = falsekey[i];
            slot.SetItem(itemBuffer.items[j]); //버튼 이미지삽입
            keySlot.Add(slot); //버튼 리스트 작성
        }

        //모듈시작 또는 타이머 시작 
        OffLight();
    }

    //신호등 초기화
    void OffLight()
    {
        //메인 신호등에 꺼짐 신호를 보낸다
        //메인 신호등이 검은색으로 된다
        //동시에 4개 버튼 신호등도 검은색이 된다
        mainLight.color = Color.black;
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i).GetChild(1);
            Image image = child.GetComponent<Image>();
            image.color = Color.black;
        }

        //비교전용 리스트도 초기화 한다
        truekey2.Clear(); //제출용 리스트로 재활용

        //버튼의 기능이 활성화 된다
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i);
            Button button = child.GetComponent<Button>();
            button.interactable = true;
        }
    }

    //신호등 작동
    //4개 각각 버튼에서 빨간불 or 초록불이 들어온다
    //누른 버튼에서 정답 리스트와 비교한다
    //리스트와 일치하면 누른 버튼 신호등이 초록불
    //리스트와 불일치하면 누른 버튼 신호등이 빨간불
    //누른 버튼의 신도등이 들어오면 해당 버튼 기능 정지
    //버튼 신호등이 4개가 되면 비교용 리스트 내용이 4개 채워진다
    //비교용 리스트내용 4개가 되면 메인신호등의 불이 들어온다
    //4개 모두 초록불이면 메인신호등이 초록불과 정답처리
    //초록불 4개가 아니면 메인신호등이 빨간불과 오답처리

    //정답처리는 키패드정지, 게임매니저에 정답누적 한다
    //오답처리는 키패드지연시간(?초), 게임매니저에 오답누적 한다
    //게임매니저 누적이 2개 이하면, 지연시간 후 신호등 초기화

}
