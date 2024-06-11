using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeypadSlot : MonoBehaviour
{
    public ItemBuffer itemBuffer; //아이템 리스트
    public Transform keyRoot; //심볼이 들어갈 장소
    private List<Slot> keySlot; //메뉴얼 심볼 리스트
    public int selectLine; //선택된 심볼들
    private List<int> truekey; //정답 리스트
    private List<int> truekey2; //작성용 리스트
    private List<int> falsekey; //문제용 리스트

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
        }
        //선택한 줄에서 4개로 정답을 만든다.
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 7 - i);
            truekey.RemoveAt(j);
            truekey2.RemoveAt(j);
        }
        //문제용 4개를 임의로 섞는다. 
        falsekey = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, 4 - i);
            falsekey.Add(truekey2[j]);
        }

        keySlot = new List<Slot>(); //슬롯 리스트 정의
        for (int i = 0; i < keyRoot.childCount; i++)
        {
            var slot = keyRoot.GetChild(i).GetComponent<Slot>();
            int j = falsekey[i];
            slot.SetItem(itemBuffer.items[j]);
            keySlot.Add(slot);
        }
    }

}
