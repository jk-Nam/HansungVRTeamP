using UnityEngine;

public class RandomMaze : MonoBehaviour
{
    void Start()
    {
        ActivateRandomChild();
    }

    void ActivateRandomChild()
    {
        // 자식 객체가 있는지 확인
        if (transform.childCount == 0)
        {
            Debug.LogError("No child objects found.");
            return;
        }

        // 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, transform.childCount);

        // 모든 자식 객체 비활성화
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // 선택된 자식 객체 활성화
        transform.GetChild(randomIndex).gameObject.SetActive(true);
    }
}
