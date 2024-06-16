using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualPng : MonoBehaviour
{
    public Transform contentTransform; // Content 오브젝트의 Transform
    public GameObject imagePrefab; // 이미지 프리팹

    void Start()
    {
        LoadPNGs();
    }

    // Resources 폴더에서 매뉴얼PNG 파일들을 로드하여 스크롤뷰에 추가하는 함수
    void LoadPNGs()
    {
        // Resources/ManualPng 폴더에서 모든 PNG 파일을 로드
        Sprite[] sprites = Resources.LoadAll<Sprite>("ManualPng");

        // 로드된 스프라이트들을 순회하면서 스크롤뷰에 추가
        foreach (Sprite sprite in sprites)
        {
            // 이미지 프리팹을 인스턴스화
            GameObject newImageObject = Instantiate(imagePrefab, contentTransform);

            // 인스턴스화된 오브젝트의 Image 컴포넌트를 가져와 스프라이트 설정
            Image imageComponent = newImageObject.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = sprite;
            }
        }
    }
}
