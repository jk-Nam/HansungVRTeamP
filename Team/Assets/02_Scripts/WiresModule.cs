using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresModule : BombModule
{
    public WiresModule()
    {
        moduleType = BombMoudleType.Wire;
    }

    Bomb bomb;
    CutTest cut;

    //public AssetReference wirePrefab;
    //public AssetReference brokenWirePrefab;
    public GameObject wirePrefab;
    public GameObject brokenWirePrebab;
    public Transform[] wirePos;

    public List<string> wireColor = new List<string> { "Red", "Blue", "Black", "White", "Yellow" };
    public List<Material> mats;

    public int correctWireNum;
    int wireCnt;

    public List<GameObject> wires = new List<GameObject>();
    public List<GameObject> brokenWires = new List<GameObject>();

    private void Awake()
    {
        bomb = GameObject.FindGameObjectWithTag("BOMB").GetComponent<Bomb>();
        cut = GetComponentInParent<CutTest>();
    }

    private void Start()
    {
        InitiallizeModule();
        DefuseModule();
    }

    //모듈 초기화
    public override void InitiallizeModule()
    {
        GameManager.Instance.defuesedCnt = 0;
        GameManager.Instance.incorrectCnt = 0;
        wireCnt = Random.Range(3, 6);
        Debug.Log("Wire의 수는 " + wireCnt + "개 입니다.");
    }

    private void SetButtonsActive()
    {
        for (int i = 0; i < wires.Count; i++)
        {
            cut.cutButtons[i].gameObject.SetActive(true);
        }
    }

    private void DisableAllButtons()
    {
        for (int i = 0; i < cut.cutButtons.Count; i++)
        {
            cut.cutButtons[i].interactable = false;
        }
    }

    public override void DefuseModule()
    {
        switch (wireCnt)
        {
            case 3: //와이어의 갯수가 3개일 경우
                List<string> ThreeColor = GetRandomColors(wireColor, 3);
                foreach (string color in ThreeColor)
                {
                    Debug.Log(color);
                }
                wireColor = ThreeColor;
                if (wireColor.Contains("Red"))
                {
                    if (wireColor[2] == "White")
                    {
                        correctWireNum = 3;
                        Debug.Log("1-1 잘라야 하는 와이어 : " + correctWireNum + "번");
                    }
                    else
                    {
                        if (wireColor.FindAll(w => w == "Blue").Count >= 2)
                        {
                            //빨 파 파, 파 빨 파, 파 파 빨
                            if (wireColor[2] == "Blue")
                            {
                                correctWireNum = 3;
                                Debug.Log("1-2 잘라야 하는 와이어 : " + correctWireNum + "번");
                            }
                            else
                            {
                                correctWireNum = 2;
                                Debug.Log("1-3 잘라야 하는 와이어 : " + correctWireNum + "번");
                            }
                        }
                        else
                        {
                            correctWireNum = 3;
                            Debug.Log("1-4 잘라야 하는 와이어 : " + correctWireNum + "번");
                        }
                    }
                }
                else
                {
                    correctWireNum = 2;
                    Debug.Log("1-5 잘라야 하는 와이어 : " + correctWireNum + "번");
                }
                break;

            case 4: //와이어의 갯수가 4개일 경우
                List<string> fourColor = GetRandomColors(wireColor, 4);
                foreach (string color in fourColor)
                {
                    Debug.Log(color);
                }
                wireColor = fourColor;
                if (wireColor.FindAll(w => w == "Red").Count >= 2)
                {
                    correctWireNum = wireColor.FindLastIndex(str => str == "Red") + 1;
                    Debug.Log("4-1 잘라야 하는 와이어 : " + correctWireNum + "번");
                }
                else
                {
                    if (wireColor[3] == "Yellow" && !wireColor.Contains("Red"))
                    {
                        correctWireNum = 1;
                        Debug.Log("4-2 잘라야 하는 와이어 : " + correctWireNum + "번");
                    }
                    else
                    {
                        if (wireColor.FindAll(w => w == "Blue").Count == 1)
                        {
                            correctWireNum = 1;
                            Debug.Log("4-3 잘라야 하는 와이어 : " + correctWireNum + "번");
                        }
                        else
                        {
                            if (wireColor.FindAll(w => w == "Yellow").Count >= 2)
                            {
                                correctWireNum = 4;
                                Debug.Log("4-4 잘라야 하는 와이어 : " + correctWireNum + "번");
                            }
                            else
                            {
                                correctWireNum = 2;
                                Debug.Log("4-5 잘라야 하는 와이어 : " + correctWireNum + "번");
                            }
                        }
                    }
                }
                break;

            case 5: //와이어의 갯수가 5개일 경우
                List<string> fiveColor = GetRandomColors(wireColor, 5);
                foreach (string color in fiveColor)
                {
                    Debug.Log(color);
                }
                wireColor = fiveColor;
                if (wireColor[4] == "Black")
                {
                    correctWireNum = 4;
                    Debug.Log("5-1 잘라야 하는 와이어 : " + correctWireNum + "번");
                }
                else
                {
                    if (wireColor.FindAll(w => w == "Red").Count == 1 && wireColor.FindAll(w => w == "Yellow").Count >= 2)
                    {
                        correctWireNum = 1;
                        Debug.Log("5-2 잘라야 하는 와이어 : " + correctWireNum + "번");
                    }
                    else
                    {
                        if (!wireColor.Contains("Black"))
                        {
                            correctWireNum = 2;
                            Debug.Log("5-3 잘라야 하는 와이어 : " + correctWireNum + "번");
                        }
                        else
                        {
                            correctWireNum = 1;
                            Debug.Log("5-4 잘라야 하는 와이어 : " + correctWireNum + "번");
                        }
                    }
                }
                break;
        }
    }

    List<string> GetRandomColors(List<string> colors, int count)
    {
        List<string> rColors = new List<string>();
        
        Dictionary<string, Material> colorToMaterial = new Dictionary<string, Material>
        {
            { "Red", mats[0] },
            { "Blue", mats[1] },
            { "Black", mats[2] },
            { "White", mats[3] },
            { "Yellow", mats[4] }
        };

        // 색깔과 Addressable 매테리얼 키 매칭
        //    Dictionary<string, string> colorToMaterialAddress = new Dictionary<string, string>
        //{
        //    { "Red",    "Assets/08_Materials/Red.mat" },
        //    { "Blue",   "Assets/08_Materials/Blue.mat" },
        //    { "Black",  "Assets/08_Materials/Black.mat" },
        //    { "White",  "Assets/08_Materials/White.mat" },
        //    { "Yellow", "Assets/08_Materials/Yellow.mat" }
        //};

        // wirePos의 길이가 count보다 작은 경우 예외 처리
        if (wirePos.Length < count)
        {
            Debug.LogError("wirePos 배열의 길이가 count보다 작습니다.");
            return rColors;
        }

        for (int i = 0; i < count; i++)
        {
            int rIdx = Random.Range(0, colors.Count);
            rColors.Add(colors[rIdx]);

            // 비동기 처리를 위한 로컬 변수로 저장
            //int localIndex = i;
            
            string selectedColor = colors[rIdx];

            // 와이어 생성
            //AsyncOperationHandle<GameObject> handle = wirePrefab.InstantiateAsync();
            GameObject wireModule = Instantiate(wirePrefab);
            wires.Add(wireModule);
            wirePos[i].gameObject.SetActive(true);
            wireModule.transform.position = wirePos[i].transform.position;
            wireModule.transform.SetParent(wirePos[i]);
            wireModule.transform.position = wirePos[i].position;
            wireModule.transform.rotation = wirePos[i].rotation;
            wireModule.transform.Rotate(0, 180.0f, 0, Space.Self);
            wireModule.transform.localScale = new Vector3(0.24f, 0.2f, 2f);

            //메테리얼 변경
            Renderer wireRenderer = wireModule.GetComponentInChildren<Renderer>();
            if (wireRenderer != null && colorToMaterial.ContainsKey(colors[rIdx]))
            {
                wireRenderer.material = colorToMaterial[colors[rIdx]];
            }

            // 절단된 와이어 생성
            //AsyncOperationHandle<GameObject> handle = wirePrefab.InstantiateAsync();
            GameObject brokenWireModule = Instantiate(brokenWirePrebab);
            brokenWires.Add(brokenWireModule);
            brokenWireModule.SetActive(false);
            brokenWireModule.transform.position = wirePos[i].transform.position;
            brokenWireModule.transform.SetParent(wirePos[i]);
            brokenWireModule.transform.position = wirePos[i].position;
            brokenWireModule.transform.rotation = wirePos[i].rotation;
            brokenWireModule.transform.Rotate(0, 180.0f, 0, Space.Self);
            brokenWireModule.transform.localScale = new Vector3(0.24f, 0.2f, 2f);

            //메테리얼 변경
            Renderer[] brokenWireRenderers = brokenWireModule.GetComponentsInChildren<Renderer>();
            foreach (Renderer brokenWireRenderer in brokenWireRenderers)
            {
                if (brokenWireRenderer != null && colorToMaterial.ContainsKey(colors[rIdx]))
                {
                    brokenWireRenderer.material = colorToMaterial[colors[rIdx]];
                }
            }


            //handle.Completed += (AsyncOperationHandle<GameObject> completeHandle) =>
            //{
            //    GameObject go_Wire = completeHandle.Result;
            //    wires.Add(go_Wire);
            //    Transform wireTransform = wirePos[localIndex];
            //    wireTransform.gameObject.SetActive(true);
            //    go_Wire.transform.SetParent(wireTransform); // 로컬 변수 사용
            //    go_Wire.transform.position = wireTransform.position;
            //    go_Wire.transform.rotation = wireTransform.rotation;
            //    go_Wire.transform.Rotate(0, 180.0f, 0, Space.Self);
            //    go_Wire.transform.localScale = new Vector3(0.25f, 0.2f, 0.2f);

            //    // 와이어 매테리얼 변경
            //    if (colorToMaterialAddress.ContainsKey(selectedColor))
            //    {
            //        string materialAddress = colorToMaterialAddress[selectedColor];
            //        Debug.Log("Loading material: " + materialAddress);
            //        Addressables.LoadAssetAsync<Material>(materialAddress).Completed += (AsyncOperationHandle<Material> matHandle) =>
            //        {
            //            if (matHandle.Status == AsyncOperationStatus.Succeeded)
            //            {
            //                Material loadedMaterial = matHandle.Result;
            //                Renderer wireRenderer = go_Wire.GetComponentInChildren<Renderer>();
            //                if (wireRenderer != null)
            //                {
            //                    wireRenderer.material = loadedMaterial;
            //                }
            //            }
            //            else
            //            {
            //                Debug.LogError("Failed to load material: " + materialAddress);
            //            }
            //        };
            //    }
            //    else
            //    {
            //        Debug.LogError("Color not found in dictionary: " + selectedColor);
            //    }

            // 절단된 와이어 생성
            //            AsyncOperationHandle<GameObject> handle2 = brokenWirePrefab.InstantiateAsync();

            //            handle2.Completed += (AsyncOperationHandle<GameObject> completeHandle) =>
            //            {
            //                GameObject go_BrokenWire = completeHandle.Result;
            //                brokenWires.Add(go_BrokenWire);
            //                Transform wireTransform = wirePos[localIndex];
            //                go_BrokenWire.gameObject.SetActive(false);
            //                go_BrokenWire.transform.SetParent(wireTransform); // 로컬 변수 사용
            //                go_BrokenWire.transform.position = wireTransform.position;
            //                go_BrokenWire.transform.rotation = wireTransform.rotation;
            //                go_BrokenWire.transform.Rotate(0, 180.0f, 0, Space.Self);
            //                go_BrokenWire.transform.localScale = new Vector3(0.25f, 0.2f, 0.2f);

            //                // 와이어 매테리얼 변경
            //                if (colorToMaterialAddress.ContainsKey(selectedColor))
            //                {
            //                    string materialAddress = colorToMaterialAddress[selectedColor];
            //                    Debug.Log("Loading material: " + materialAddress);
            //                    Addressables.LoadAssetAsync<Material>(materialAddress).Completed += (AsyncOperationHandle<Material> matHandle) =>
            //                    {
            //                        if (matHandle.Status == AsyncOperationStatus.Succeeded)
            //                        {
            //                            Material loadedMaterial = matHandle.Result;
            //                            Renderer wireRenderer = go_BrokenWire.GetComponentInChildren<Renderer>();
            //                            if (wireRenderer != null)
            //                            {
            //                                wireRenderer.material = loadedMaterial;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            Debug.LogError("Failed to load material: " + materialAddress);
            //                        }
            //                    };
            //                }
            //                else
            //                {
            //                    Debug.LogError("Color not found in dictionary: " + selectedColor);
            //                }
            //            };
            //        };

        }

        SetButtonsActive();
        //cut.OnClickSetUp();
        return rColors;
    }

    public void CutWire(int idx)
    {
        if (!isDefused && GameManager.Instance.isGameStart && !GameManager.Instance.isGameOver)
        {
            Debug.Log(idx + "번 클릭");
            //프리팹 변경
            wires[idx - 1].SetActive(false);
            brokenWires[idx - 1].SetActive(true);

            if (idx == correctWireNum)
            {
                isDefused = true;
                GameManager.Instance.defuesedCnt++;
                Debug.Log("와이어 모듈 해제 성공");
                if (GameManager.Instance.defuesedCnt == GameManager.Instance.totalModuleCnt)
                {
                    GameManager.Instance.GameClear();
                }
            }
            else
            {
                GameManager.Instance.incorrectCnt++;
                Debug.Log("오답 수 : " + GameManager.Instance.incorrectCnt);
                if (GameManager.Instance.incorrectCnt >= 3)
                {
                    bomb.Fail();
                    Debug.Log("Game Over!!!");
                }
            }
        }
        else
        {
            Debug.Log("클릭 처리 안되는 중");

        }
    }
}
