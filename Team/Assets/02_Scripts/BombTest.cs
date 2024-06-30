using UnityEngine;
using System.Collections.Generic;

public class BombTest : MonoBehaviour
{
    public GameObject[] objectPrefabs; // 여러 프리팹을 저장할 배열
    public Transform[] positions; // 각 면에 배치할 위치들

    void Start()
    {
        CreateAllObjects();
        Debug.Log("여기!!!!!!!!!!!카운트"+GameManager.Instance.totalModuleCnt);
        
    }

    void CreateAllObjects()
    {
        if (objectPrefabs.Length == 0 || positions.Length == 0)
        {
            Debug.LogError("마따끄 프리팹이랑 포지션이 없잖아!");
            return;
        }

        List<int> usedPositions = new List<int>();

        for (int i = 0; i < GameManager.Instance.totalModuleCnt; i++)
        {
            // 랜덤한 faceIndex를 선택
            int faceIndex;
            do
            {
                faceIndex = Random.Range(0, positions.Length);
            } while (usedPositions.Contains(faceIndex));

            // 사용된 위치에 추가
            usedPositions.Add(faceIndex);

            // 현재 프리팹 선택
            GameObject currentPrefab = objectPrefabs[i];

            // 선택된 위치
            Transform parentTransform = positions[faceIndex];

            // 오브젝트를 부모 Transform의 자식으로 생성
            GameObject newObj = Instantiate(currentPrefab, parentTransform);

            //폭탄 크기에 맞게 올바른 위치로 생성 되게 하는 코드. 수동으로 조정 할 
            //float halfScale = transform.localScale.y*0.5f + 0.01f;

            // 선택된 faceIndex에 따라 위치 및 회전 설정
            switch (faceIndex)
            {
                case 0: // 위쪽 면
                    newObj.transform.localPosition = new Vector3(0, 0.51f, 0);
                    newObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    break;
                case 1: // 아래쪽 면
                    newObj.transform.localPosition = new Vector3(0, -0.51f, 0);
                    newObj.transform.localRotation = Quaternion.Euler(270, 0, 0);
                    break;
                case 2: // 뒤쪽 면
                    newObj.transform.localPosition = new Vector3(0, 0, -0.51f);
                    newObj.transform.localRotation = Quaternion.Euler(180, 180, 0);
                    break;
                case 3: // 왼쪽 면
                    newObj.transform.localPosition = new Vector3(-0.51f, 0, 0);
                    newObj.transform.localRotation = Quaternion.Euler(180, -90, 0);
                    break;
                case 4: // 오른쪽 면
                    newObj.transform.localPosition = new Vector3(0.51f, 0, 0);
                    newObj.transform.localRotation = Quaternion.Euler(180, 90, 0);
                    break;
            }
        }
    }
}
