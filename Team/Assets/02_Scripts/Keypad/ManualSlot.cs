using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManualSlot : MonoBehaviour
{
    //메뉴얼에 나온 문자열을 리스트로 저장하는 스크립트 입니다.
    public ItemBuffer itemBuffer; //아이템 리스트
    public Transform mRoot; //심볼이 들어갈 장소
    private List<Slot> mSlot; //메뉴얼 심볼 리스트

    void Start()
    {
        itemBuffer = GameObject.Find("KeypadScript").GetComponent<ItemBuffer>();

        mSlot = new List<Slot>(); //슬롯 리스트 정의
        //넣어야 할 문양번호 리스트
        List<int> numbers = new List<int>() 
        {   28, 13, 30, 12, 7, 9, 23,
            16, 28, 23, 26, 3, 9, 20,
            1, 8, 26, 5, 15, 30, 3,
            11, 21, 31, 7, 5, 20, 4,
            24, 4, 31, 22, 21, 19, 2,
            11, 16, 27, 14, 24, 18, 6 
        };

        //문양번호 리스트에 나온내용을 슬롯리스트에 넣는다.
        for (int i = 0; i < mRoot.childCount; i++)
        {
            //대상슬롯은 i번째 슬롯의 컨포넌트 
            var slot = mRoot.GetChild(i).GetComponent<Slot>();
            int j = numbers[i];
            slot.SetItem(itemBuffer.items[j]);

            mSlot.Add(slot);
        }

        //6줄 중에서 랜덤으로 1줄 선택 한다
        //선택한 줄에서 4개를 추출하여 정답을 준비한다
        //4개를 버튼에 랜덤 삽입한다.
        //버튼을 누르면 정답과 비교한다.
    }
}
