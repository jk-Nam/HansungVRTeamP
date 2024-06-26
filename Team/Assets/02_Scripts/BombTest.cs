using UnityEngine;

public class BombTest : MonoBehaviour
{
    public GameObject[] objectPrefabs; // 여러 프리팹을 저장할 배열
    public Transform[] positions; // 각 면에 배치할 위치들

    void Start()
    {
        CreateRandomObject();
    }

    void CreateRandomObject()
    {
        if (objectPrefabs.Length == 0 || positions.Length == 0)
        {
            Debug.LogError("objectPrefabs 배열과 positions 배열이 적절히 초기화되어야 합니다.");
            return;
        }

        // 랜덤한 faceIndex를 선택
        int faceIndex = Random.Range(2, positions.Length);

        // 랜덤한 프리팹 선택
        GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // 선택된 위치
        Transform parentTransform = positions[faceIndex];

        // 오브젝트를 부모 Transform의 자식으로 생성
        GameObject newObj = Instantiate(randomPrefab, parentTransform);

        // 선택된 faceIndex에 따라 위치 및 회전 설정
        switch (faceIndex)
        {
            //case 0: // 위쪽 면
            //    newObj.transform.localPosition = new Vector3(0, 0.51f, 0);
            //    newObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
            //    break;
            //case 1: // 아래쪽 면
            //    newObj.transform.localPosition = new Vector3(0, -0.51f, 0);
            //    newObj.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            //    break;
            //case 2: // 앞쪽 면
            //    newObj.transform.localPosition = new Vector3(0, 0, 0.51f);
            //    newObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //    break;
            //case 3: // 뒤쪽 면
            //    newObj.transform.localPosition = new Vector3(0, 0, -0.51f);
            //    newObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //    break;
            //case 4: // 왼쪽 면
            //    newObj.transform.localPosition = new Vector3(-0.51f, 0, 0);
            //    newObj.transform.localRotation = Quaternion.Euler(0, -90, 0);
            //    break;
            //case 5: // 오른쪽 면
            //    newObj.transform.localPosition = new Vector3(0.51f, 0, 0);
            //    newObj.transform.localRotation = Quaternion.Euler(0, 90, 0);
            //    break;

            case 0: // 위쪽 면
                newObj.transform.localPosition = new Vector3(0, 0.51f, 0);
                newObj.transform.localRotation = Quaternion.Euler(270, 0, 0);
                break;
            case 1: // 아래쪽 면
                newObj.transform.localPosition = new Vector3(0, -0.51f, 0);
                newObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
                break;
            case 2: // 앞쪽 면
                newObj.transform.localPosition = new Vector3(0, 0, 0.51f);
                newObj.transform.localRotation = Quaternion.Euler(180, 0, 0);
                break;
            case 3: // 뒤쪽 면
                newObj.transform.localPosition = new Vector3(0, 0, -0.51f);
                newObj.transform.localRotation = Quaternion.Euler(180, 180, 0);
                break;
            case 4: // 왼쪽 면
                newObj.transform.localPosition = new Vector3(-0.51f, 0, 0);
                newObj.transform.localRotation = Quaternion.Euler(180, -90, 0);
                break;
            case 5: // 오른쪽 면
                newObj.transform.localPosition = new Vector3(0.51f, 0, 0);
                newObj.transform.localRotation = Quaternion.Euler(180, 90, 0);
                break;
        }
    }
}
