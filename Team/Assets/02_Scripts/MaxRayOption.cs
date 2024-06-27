using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Surfaces;
using Oculus.Interaction;

public class MaxRayOption : MonoBehaviour
{
    //이 코드는 SceneMgr의 부족한 부분을 보완하기 위한 코드입니다.


    public RayInteractor leftRay; //왼손
    public RayInteractor rightRay; //오른손

    public float rayDefault = 0.1f; //초기화 값
    public float rayMax = 20.0f; //연장값

    //Ray 길이 초기화
    public void RayDefault()
    {
        leftRay.MaxRayLength = rightRay.MaxRayLength = rayDefault;
    }

    //Ray 길이 연장
    public void RayMax()
    {
        leftRay.MaxRayLength = rightRay.MaxRayLength = rayMax;
    }
}