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

    public string player1ID;
    public string player2ID;
    public string resultID;
    public int difficultyNum = 1;

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

    IEnumerator Login(string playerId)
    {
        WWWForm form = new WWWForm();
        form.AddField("_id", playerId);

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
                Debug.Log("로그인 성공!!!");
                StartCoroutine(GetUserInfo(playerId));
            }
            else
            {
                Debug.Log("로그인 실패!!!");
            }
        }
    }

    IEnumerator CheckUserId()
    {
        // Check if we have saved PlayerID in PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            player1ID = PlayerPrefs.GetString("PlayerID");
            yield return StartCoroutine(Login(player1ID));
        }
        else
        {
            yield return StartCoroutine(Signin());
        }
    }

    IEnumerator Signin()
    {
        UnityWebRequest www = UnityWebRequest.PostWwwForm(signInURL, "");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonData = JSON.Parse(www.downloadHandler.text);
            player1ID = jsonData["userId"];
            Debug.Log("고유 아이디 생성 완료: " + player1ID);

            // Save PlayerID to PlayerPrefs
            PlayerPrefs.SetString("PlayerID", player1ID);

            yield return StartCoroutine(Login(player1ID));
        }
    }

    public void OnClickResult()
    {
        StartCoroutine(Result());
    }

    public IEnumerator Result()
    {
        WWWForm form = new WWWForm();

        form.AddField("player1", player1ID);
        form.AddField("player2", player2ID);
        form.AddField("timer", GameManager.Instance.timer.ToString());
        form.AddField("isclear", GameManager.Instance.isClear.ToString());
        form.AddField("difficulty", Convert.ToInt32(GameManager.Instance.difficulty));

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
            //var jsonData = SimpleJSON.JSON.Parse(www.downloadHandler.text);
            //if (www.downloadHandler.text != "")
            //{
            //    resultID = jsonData["_id"];                
            //}


            //UnityWebRequest www2 = UnityWebRequest.Post(userInfoURL, form);
            //yield return www2.SendWebRequest();
            //var jsonData = SimpleJSON.JSON.Parse(www2.downloadHandler.text);
            //Debug.Log(www2.downloadHandler.text);
            //if (www2.downloadHandler.text != "")
            //{
            //    //player1IDText.text = jsonData["player1"];
            //    //player2IDText.text = jsonData["player2"];

            //}
        }
    }

    IEnumerator GetUserInfo(string playerId)
    {
        WWWForm form = new WWWForm();
        form.AddField("_id", playerId);

        UnityWebRequest www = UnityWebRequest.Post(userInfoURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonData = JSON.Parse(www.downloadHandler.text);
            Debug.Log("유저 정보: " + jsonData.ToString());
            // Handle user info as needed
        }
    }

    public IEnumerator UpdateData()
    {
        WWWForm form = new WWWForm();

        form.AddField("_id", resultID);
        form.AddField("timer", GameManager.Instance.timer.ToString("n2"));
        form.AddField("isclear", GameManager.Instance.isClear.ToString());
        form.AddField("difficulty", Convert.ToInt32(GameManager.Instance.difficulty).ToString());

        UnityWebRequest www = UnityWebRequest.Post(updateURL, form);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);

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

    public void OnClickEasyRankingBtn()
    {
        difficultyNum = 1;
        StartCoroutine(GetRanking());
    }

    public void OnClickHardRankingBtn()
    {
        difficultyNum = 2;
        StartCoroutine(GetRanking());
    }

    IEnumerator GetRanking()
    {
        WWWForm form = new WWWForm();

        //form.AddField("userId", PlayerPrefs.GetString("PlayerID"));
        form.AddField("difficulty", difficultyNum); //난이도 별로 랭킹 보여주기

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
            //for (int i = 0; i < rankingCnt; i++)
            //{
            //    Debug.Log(rankingJson[i]["player1"]);
            //    Debug.Log(rankingJson[i]["player2"]);
            //    Debug.Log(rankingJson[i]["timer"]);
            //    Debug.Log(rankingJson[i]["difficulty"]);
            //}
        }
    }
}
