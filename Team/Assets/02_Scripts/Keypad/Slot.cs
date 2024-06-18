using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //[HideInInspector]
    // 해당 아이템 선언?
    public ItemProperty item;
    public ItemBuffer itemBuffer; //아이템 리스트

    public Image image; // 변하는 이미지 선언

    //아이템 장착 함수 정의 (아이템 정보)
    public void SetItem(ItemProperty item)
    {
        //현재 아이템에 아이템 정보를 넣는다
        this.item = item;

        //Debug.Log(item + " 값으로 처리");

        //아이템값이 널인가?
        if (item == null || item.name == "Empty")
        {
            //이미지 사용을 비활성화
            image.enabled = false;
            //아이템이 없으면 "Empty" 넣는다
            gameObject.name = "Empty";
        }
        else //아이템값이 있으면
        {
            //이미지 사용을 활성화
            image.enabled = true; 

            //해당 개제 이름에 아이템 이름을 넣는다
            gameObject.name = item.name;
            //이미지에 아이템 이미지를 넣는다
            image.sprite = item.sprite;

            
        }

    }

    //아이템 제거 함수를 정의한다
    public void OnClickSellBtn()
    {
        //아이템 넣는 함수에 널값을 넣는다
        //SetItem(null);

        //아이템에 빈값설정 아이템을 넣는다
        SetItem(itemBuffer.items[0]);
    }
}
