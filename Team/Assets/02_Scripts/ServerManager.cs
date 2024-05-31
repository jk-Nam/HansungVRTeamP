using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;


public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance;

    public long uniqueId;

    [Header("===URLS===")]
    public string loginURL;
    public string signInURL;
    public string checkIdURL;
    public string updateURL;
    public string userInfoURL;
    public string rankingURL;

    [Header("===Panels===")]
    public GameObject playerPanel;
    public GameObject rankingPanel;

    [Header("===Variables===")]
    public float timer;

    [Header("===UI_Text===")]
    public Text playerUniqueID;
    public Text timerText;

    int rankingCnt = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        //ObjectID �ߺ� Ȯ��
        //if ()
        //{
        //    uniqueId = long.Parse(PlayerPrefs.GetString("UniqueID"));
        //}
        //else
        //{
        //    uniqueId = 
        //}
    }

    private void Start()
    {
        
    }

    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", uniqueId.ToString());

        UnityWebRequest www = UnityWebRequest.Post(loginURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text == "Success")
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("�α��� ����!!!");
                PlayerPrefs.SetString("PlayerID", uniqueId.ToString());             
            }
            else
            {
                Debug.Log("�α��� ����!!!");
            }
        }
    }

    IEnumerator CheckUserId()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", uniqueId.ToString());

        UnityWebRequest www = UnityWebRequest.Post(checkIdURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text == "Exist")
            {
                Debug.Log("���̵� �ߺ�");
                //StartCoroutine(Signin());
            }
        }
    }

    IEnumerator Signin()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", uniqueId.ToString());

        UnityWebRequest www = UnityWebRequest.Post(signInURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("���� ���̵� ���� �Ϸ�");
        }
    }

    //ObjectID �޾ƿ���
    //IEnumerator GetUserInfo()
    //{
    //    WWWForm form = new WWWForm();

    //    form.AddField("_id")
    //}

    IEnumerator GetRanking()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", PlayerPrefs.GetString("PlayerID"));

        UnityWebRequest www = UnityWebRequest.Post(rankingURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            var rankingJson = JSON.Parse(www.downloadHandler.text);
            rankingCnt = rankingJson.Count;
            for (int i = 0; i < rankingCnt; i++)
            {
                Debug.Log(rankingJson[i]["userId"]);
                Debug.Log(rankingJson[i]["timer"]);
            }
        }
    }
}
