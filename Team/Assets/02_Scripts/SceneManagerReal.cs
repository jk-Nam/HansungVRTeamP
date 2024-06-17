using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerReal : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static SceneManagerReal instance;

    // 현재 씬의 이름
    private string currentSceneName;

    #region 싱글톤
    public string CurrentSceneName
    {
        get { return currentSceneName; }
    }

    
    public static SceneManagerReal Instance
    {
        get
        {
            if (instance == null)
            {
               
                instance = FindObjectOfType<SceneManagerReal>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SceneManagerSingleton");
                    instance = singletonObject.AddComponent<SceneManagerReal>();
                }

                
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
   

    void Awake()
    {
    
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    #endregion

    // 씬을 로드하는 메서드
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainSceneRay");
    }

    

    public void QuitGame()
    {
        Application.Quit();
    }
}
