using UnityEngine;
using Photon.Pun;

public class GameOverPhoton : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void GameOverRPC()
    {
        // GameManager 스크립트의 GameOver 호출
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.LogError("GameManager 스크립트를 찾을 수 없습니다.");
        }
    }
}