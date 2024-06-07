using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using System;


public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance;

    public string uniqueId;

    [Header("===URLS===")]
    public string loginURL;
    public string signInURL;
    public string checkIdURL;
    public string newResultURL;
    public string updateURL;
    public string userInfoURL;
    public string rankingURL;

    [Header("===Panels===")]
    public GameObject playerPanel;
    public GameObject rankingPanel;

    [Header("===Variables===")]
    public float timer;
    public bool isClear;
    

    [Header("===UI_Text===")]
    public Text playerUniqueIDText;
    public Text timerText;
    public Text player1IDText;
    public Text player2IDText;

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
    }

    private void Start()
    {
        //ObjectID 중복 확인
        StartCoroutine(CheckUserId());
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
                Debug.Log("로그인 성공!!!");
                PlayerPrefs.SetString("PlayerID", uniqueId.ToString());
                StartCoroutine(GetUserInfo());
            }
            else
            {
                Debug.Log("로그인 실패!!!");
            }
        }
    }

    IEnumerator CheckUserId()
    {
        WWWForm form = new WWWForm();

        //form.AddField("userId", uniqueId);

        UnityWebRequest www = UnityWebRequest.Post(checkIdURL, form);
        yield return www.SendWebRequest();
        UnityWebRequest www2 = UnityWebRequest.Post(userInfoURL, form);
        yield return www2.SendWebRequest();
        var jsonData = SimpleJSON.JSON.Parse(www2.downloadHandler.text);
        Debug.Log(www2.downloadHandler.text);
        if (www2.downloadHandler.text !="")
        {
            uniqueId = jsonData["_id"];
        }
        

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text == "Exist")
            {
                Debug.Log("아이디 중복");
                StartCoroutine(LogIn());
            }
            else
            {
                Debug.Log("아이디 생성");
                StartCoroutine(Signin());
            }
        }
    }

    IEnumerator Signin()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", uniqueId);

        UnityWebRequest www = UnityWebRequest.Post(signInURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("고유 아이디 생성 완료");
            UnityWebRequest www2 = UnityWebRequest.Post(userInfoURL, form);
            yield return www2.SendWebRequest();
            var jsonData = SimpleJSON.JSON.Parse(www2.downloadHandler.text);
            Debug.Log(www2.downloadHandler.text);
            if (www2.downloadHandler.text != "")
            {
                uniqueId = jsonData["_id"];
            }
            StartCoroutine(LogIn());
        }
    }

    public void OnClickResult()
    {
        StartCoroutine(Result());
    }

    IEnumerator Result()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", uniqueId);

        UnityWebRequest www = UnityWebRequest.Post(newResultURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("게임 결과 저장 완료");
            UnityWebRequest www2 = UnityWebRequest.Post(userInfoURL, form);
            yield return www2.SendWebRequest();
            var jsonData = SimpleJSON.JSON.Parse(www2.downloadHandler.text);
            Debug.Log(www2.downloadHandler.text);
            if (www2.downloadHandler.text != "")
            {
                //player1IDText.text = jsonData["id1"];
                //player2IDText.text = jsonData["id2"];

            }
        }
    }

    IEnumerator GetUserInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("userId", PlayerPrefs.GetString("PlayerID"));
        
        UnityWebRequest www = UnityWebRequest.Post(userInfoURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            playerUniqueIDText.text = "ID : " + PlayerPrefs.GetString("PlayerID");
            var jsonData = SimpleJSON.JSON.Parse(www.downloadHandler.text);
            timer = jsonData["timer"];
            isClear = jsonData["isclear"];

            timerText.text = timer.ToString();
        }
    }

    public IEnumerator UpdateDate()
    {
        WWWForm form = new WWWForm();

        form.AddField("timer", GameManager.Instance.timer.ToString());
        form.AddField("isclear",GameManager.Instance.isClear.ToString());
        form.AddField("difficulty", Convert.ToInt32(GameManager.Instance.difficulty).ToString());

        UnityWebRequest www = UnityWebRequest.Post(updateURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text == "Success")
            {
                Debug.Log("업데이트 성공!!!");
            }
            else
            {
                Debug.Log("업데이트 실패!!!");
            }
        }
    }

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
                Debug.Log(rankingJson[i]["player1"]);
                Debug.Log(rankingJson[i]["player2"]);
                Debug.Log(rankingJson[i]["timer"]);
                Debug.Log(rankingJson[i]["difficulty"]);
            }
        }
    }
}
