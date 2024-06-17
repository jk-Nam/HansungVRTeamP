using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManualPng : MonoBehaviour
{
    public GameObject scrollViewContent; // 스크롤뷰의 Content 부분에 연결할 게임 오브젝트
    public GameObject imagePrefab; // 이미지를 표시할 프리팹 (Image 컴포넌트를 가진 UI 요소)

    void Start()
    {
        LoadImages();
    }

    void LoadImages()
    {
        // Resources/ManualPng 폴더 경로
        string folderPath = Path.Combine(Application.dataPath, "Resources/ManualPng");

        // 폴더가 존재하는지 확인
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("폴더를 찾을 수 없습니다: " + folderPath);
            return;
        }

        // 폴더 내 모든 PNG 파일 경로를 가져옴
        string[] filePaths = Directory.GetFiles(folderPath, "*.png");

        foreach (string filePath in filePaths)
        {
            // 파일 이름에서 확장자를 제거한 리소스 이름을 가져옴
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // Resources 폴더 경로를 기준으로 Texture2D 로드
            Texture2D texture = Resources.Load<Texture2D>("ManualPng/" + fileName);
            if (texture != null)
            {
                // Texture2D를 Sprite로 변환
                Sprite sprite = TextureToSprite(texture);

                // Image 프리팹을 인스턴스화
                GameObject newImage = Instantiate(imagePrefab, scrollViewContent.transform);

                // Image 컴포넌트에 스프라이트 설정
                Image imgComponent = newImage.GetComponent<Image>();
                if (imgComponent != null)
                {
                    imgComponent.sprite = sprite;
                }
            }
            else
            {
                Debug.LogWarning("텍스처를 로드할 수 없습니다: " + fileName);
            }
        }

        for (int i = 0; i < scrollViewContent.transform.childCount; i++)
        {
            Transform child = scrollViewContent.transform.GetChild(i).GetChild(0);
            Text page = child.GetComponent<Text>();
            if (i != 0) 
            { 
                page.text = "Page " + i + " of " + scrollViewContent.transform.childCount;
            } 
        }
    }

    // Texture2D를 Sprite로 변환하는 함수
    Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    // 메뉴얼북 다시 불러오기
    public void ReLoadBook()
    {
        for (int i = 0; i < scrollViewContent.transform.childCount; i++)
        {
            Destroy(scrollViewContent.transform.GetChild(i).gameObject);
        }

        LoadImages();

        Debug.LogWarning("복구완료");
    }
}