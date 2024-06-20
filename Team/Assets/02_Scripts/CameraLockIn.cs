using System.Collections;
using UnityEngine;

public class CameraLockIn : MonoBehaviour
{

   
    public GameObject Player;  // 바라볼 대상 (플레이어)
    public float rotationSpeed = 2f;  // 회전 속도

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(SmoothLookAt(Player.transform));
    }

    

    IEnumerator SmoothLookAt(Transform target)
    {
        while (true)
        {
            // 타겟의 방향을 계산
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 다음 프레임까지 대기
            yield return null;
        }
    }
}

