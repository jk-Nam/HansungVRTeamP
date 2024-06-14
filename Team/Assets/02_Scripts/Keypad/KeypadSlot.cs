using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadSlot : MonoBehaviour
{
    public GameManager gameManager; //게임매니저 연동

    public ItemBuffer itemBuffer; //아이템 리스트
    public GameObject keyRoot; //심볼이 들어갈 장소
    private List<Slot> keySlot; //메뉴얼 심볼 리스트
    public int selectLine; //선택된 심볼들
    private List<int> truekey; //정답 리스트
    private List<int> truekey2; //작성용 리스트
    private List<int> falsekey; //문제용 리스트

    public GameObject mainLight; //메인 신호등
    private bool isRed = false; //키패드 정답확인

    private int clickKey = 0; //누른 버튼들의 수
    private int itemName; //버튼 아이템 이름

    public float delayTime = 1.0f; //오답 후 지연시간
    public int incorrectMax = 3; //오답 최대치
    //public int defuesedMax = 2; //정답 최대치
    public bool isKeypadFail = false;//폭파 원인 제공

    public bool isKeyReady = false; //게임준비상태

    void Start()
    {
        itemBuffer = GameObject.Find("KeypadScript").GetComponent<ItemBuffer>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //게임매니저 누적값 초기화 확인
        if (gameManager.incorrectCnt == 0 && gameManager.defuesedCnt == 0)
        {
            //게임 준비
            ReStart();
            //OffLight();
        }
    }

    void Update()
    {
        //게임매니저의 스타트를 확인한다.
        if (gameManager.isGameStart && isKeyReady)
        {
            isKeyReady = false; //새 게임에 1번씩만 허용
            //defuesedMax = gameManager.totalModuleCnt; //정답 최대치
            OffLight(); //스타트와 동시에 버튼 활성화
        }

        //게임오버가 감지되거나 오답 최대치가 되면 멈춤
        if (gameManager.isGameOver || gameManager.incorrectCnt >= incorrectMax)
        {
            GameStop();
        }
    }

    //새 게임시 호출하면 문제를 생성한다
    void ReStart()
    {
        //6줄 중에서 랜덤으로 1줄 선택 한다
        selectLine = Random.Range(0, 6);

        //넣어야 할 문양번호 리스트
        List<int> numbers = new List<int>()
        {   28, 13, 30, 12, 7, 9, 23,
            16, 28, 23, 26, 3, 9, 20,
            1, 8, 26, 5, 15, 30, 3,
            11, 21, 31, 7, 5, 20, 4,
            24, 4, 31, 22, 21, 19, 2,
            11, 16, 27, 14, 24, 18, 6
        };

        //선택한 줄을 리스트로 만든다.
        truekey = new List<int>();
        truekey2 = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            truekey.Add(numbers[i + (7 * selectLine)]);
            truekey2.Add(numbers[i + (7 * selectLine)]);
        }
        //선택한 줄에서 4개로 정답을 만든다.
        for (int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, 7 - i);
            truekey.RemoveAt(j);
            truekey2.RemoveAt(j); //정답리스트 2개 작성
        }
        //문제용 4개를 임의로 섞는다. 
        falsekey = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, 4 - i);
            falsekey.Add(truekey2[j]); //키패드버튼
            truekey2.RemoveAt(j); //사용한 심볼은 제거
        }

        keySlot = new List<Slot>(); //슬롯 리스트 정의
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            var slot = keyRoot.transform.GetChild(i).GetComponent<Slot>();
            int j = falsekey[i];
            slot.SetItem(itemBuffer.items[j]); //버튼 이미지삽입
            keySlot.Add(slot); //버튼 리스트 작성
        }

        isKeypadFail = false; //원인제공 초기화
        isKeyReady = true; //키패드 준비완료
    }

    //신호등 초기화
    void OffLight()
    {
        //메인 신호등에 꺼짐 신호를 보낸다
        //메인 신호등이 검은색으로 된다
        mainLight.GetComponent<Image>().color = Color.black;
        //동시에 4개 버튼 신호등도 검은색이 된다
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i).GetChild(1);
            Image mark = child.GetComponent<Image>();
            mark.color = Color.black;
        }
        isRed = false; //메인 신호등을 초기화 한다.

        //비교전용 리스트도 초기화 한다
        //truekey2.Clear(); //제출용 리스트로 재활용

        //버튼의 기능이 활성화 된다
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i);
            Button button = child.GetComponent<Button>();
            button.interactable = true;
        }

        clickKey = 0; //누른 버튼이 없을 때
    }

    //신호등 작동
    //4개 각각 버튼에서 빨간불 or 초록불이 들어온다
    public void OnClickKey(Slot slot)
    {
        itemName = int.Parse(slot.item.name); //입력 확인용
        Transform child = slot.transform.GetChild(1); //신호등
        Image mark = child.GetComponent<Image>(); //이미지설정
        //누른 버튼에서 정답 리스트와 비교한다
        if (truekey[clickKey] == itemName)
        {
            //리스트와 일치하면 누른 버튼 신호등이 초록불
            mark.color = Color.green;
        }
        else
        {
            //리스트와 불일치하면 누른 버튼 신호등이 빨간불
            mark.color = Color.red;
            isRed = true;
        }

        //누른 버튼의 신도등이 들어오면 해당 버튼 기능 정지
        Button button = slot.GetComponent<Button>();
        button.interactable = false; //버튼 비활성화
        clickKey++; //버튼을 누르면 증가

        //버튼 신호등이 4개가 되면 비교용 리스트 내용이 4개 채워진다
        //truekey2.Add(itemName);
        if (clickKey == 4)
        {
            MainLightOn();
        }
    }
    //비교용 리스트내용 4개가 되면 메인신호등의 불이 들어온다
    void MainLightOn()
    {
        if (!isRed)
        {
            mainLight.GetComponent<Image>().color = Color.green;
            //4개 모두 초록불이면 메인신호등이 초록불과
            gameManager.defuesedCnt++; //정답처리
            //정답처리는 키패드정지, 게임매니저에 정답누적 한다
            //누적된 정답이 최대치가 되면 알려준다.
            if (gameManager.defuesedCnt == gameManager.totalModuleCnt)
            {
                Debug.Log("모든 모듈을 풀었습니다.");
            }
            else
            {
                Debug.Log("키패드 모듈을 풀었습니다.");
            }
        }
        else
        {
            mainLight.GetComponent<Image>().color = Color.red;
            //초록불 4개가 아니면 메인신호등이 빨간불과
            gameManager.incorrectCnt++; //오답처리
            //게임매니저에 오답누적 한다
            isKeypadFail = true; //폭파원인

            StartCoroutine(ErrorEvent()); //오답 이벤트
        }
    }

    IEnumerator ErrorEvent()
    {
        //게임매니저 누적 여유가 있으면, 지연시간 후 신호등 초기화
        yield return new WaitForSecondsRealtime(delayTime);
        if (gameManager.incorrectCnt < incorrectMax )
        {
            OffLight(); //재도전
        }
        else
        {
            GameStop(); //게임중단
        }
    }

    //게임오버 이벤트
    void GameStop()
    {
        //버튼의 기능이 비활성화 된다
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i);
            Button button = child.GetComponent<Button>();
            button.interactable = false;
        }

        // 빨간불이 들어온다
        mainLight.GetComponent<Image>().color = Color.red;
        for (int i = 0; i < keyRoot.transform.childCount; i++)
        {
            Transform child = keyRoot.transform.GetChild(i).GetChild(1);
            Image mark = child.GetComponent<Image>();
            mark.color = Color.red;
        }

        //게임매니저의 타이머를 중지해야 한다
        if (gameManager.isGameStart)
        {
            gameManager.GameOver(); //게임 매니저 게임오버
        }

        if (isKeypadFail)
        {
            Debug.Log("폭파 원인 : 키패드"); 
            //관련문구 추가반영
        }
    }

}
