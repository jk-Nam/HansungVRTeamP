using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressBtn : MonoBehaviour
{
    public GameObject press;

    public Transform originPos;


    void Start()
    {
        DOTween.Init();
        
    }


    public void Press()
    {
        if (GameManager.Instance.isClear)
        {
            originPos.position = press.transform.localPosition;
            press.transform.DOLocalMove(new Vector3(originPos.position.x, (originPos.position.y + 0.07f),originPos.position.z), 0.5f)
                        .OnComplete(() =>
                        {
                            // 0.5초 후 다시 원래 로컬 위치로 이동
                            press.transform.DOLocalMove(originPos.position, 0.5f);
                        });
        }        
    }
}
