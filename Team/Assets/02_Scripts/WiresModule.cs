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

    public List<string> wireColor = new List<string> { "Red", "Blue", "Black", "White", "Yellow"};

    int wireCnt;
    public int correctWireNum;

    private void Awake()
    {
        bomb = GameObject.FindGameObjectWithTag("BOMB").GetComponent<Bomb>();
    }


    private void Start()
    {
        InitiallizeModule();
        DefuseModule();
    }

    public override void InitiallizeModule()
    {
        GameManager.Instance.defuesedCnt = 0;
        GameManager.Instance.incorrectCnt = 0;
        wireCnt = Random.Range(3, 6);
        //wireCnt = 5;
        Debug.Log("Wire�� ���� " + wireCnt + "�� �Դϴ�.");
    }

    public override void DefuseModule()
    {
        switch (wireCnt)
        {
            case 3:
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
                        Debug.Log("1-1 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                    }
                    else
                    {
                        if (wireColor.FindAll(w => w == "Blue").Count >= 2)
                        {
                            //�� �� ��, �� �� ��, �� �� ��
                            if (wireColor[2] == "Blue")
                            {
                                correctWireNum = 3;
                                Debug.Log("1-2 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                            }
                            else
                            {
                                correctWireNum = 2;
                                Debug.Log("1-3 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                            }
                        }
                        else
                        {
                            correctWireNum = 3;
                            Debug.Log("1-4 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                        }
                    }
                }
                else
                {
                    correctWireNum = 2;
                    Debug.Log("1-5 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                }
                    break;
            case 4:
                List<string> fourColor = GetRandomColors(wireColor, 4);
                foreach (string color in fourColor)
                {
                    Debug.Log(color);
                }
                wireColor = fourColor;
                if (wireColor.FindAll(w => w == "Red").Count >= 2)
                {
                    correctWireNum = wireColor.FindLastIndex(str => str == "Red") + 1;
                    Debug.Log("4-1 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                }
                else
                {
                    if (wireColor[3] == "Yellow" && !wireColor.Contains("Red"))
                    {
                        correctWireNum = 1;
                        Debug.Log("4-2 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                    }
                    else
                    {
                        if(wireColor.FindAll(w => w == "Blue").Count == 1)
                        {
                            correctWireNum = 1;
                            Debug.Log("4-3 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                        }
                        else
                        {
                            if (wireColor.FindAll(w => w == "Yellow").Count >= 2)
                            {
                                correctWireNum = 4;
                                Debug.Log("4-4 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                            }
                            correctWireNum = 2;
                            Debug.Log("4-5 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                        }
                    }
                }
                break;
            case 5:
                List<string> fiveColor = GetRandomColors(wireColor, 5);
                foreach (string color in fiveColor)
                {
                    Debug.Log(color);
                }
                wireColor = fiveColor;
                if (wireColor[4] == "Black")
                {
                    correctWireNum = 4;
                    Debug.Log("5-1 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                }
                else
                {
                    if (wireColor.FindAll(w => w == "Red").Count == 1 && wireColor.FindAll(w => w == "Yellow").Count >= 2)
                    {
                        correctWireNum = 1;
                        Debug.Log("5-2 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                    }
                    else
                    {
                        if (!wireColor.Contains("Black"))
                        {
                            correctWireNum = 2;
                            Debug.Log("5-3 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                        }
                        else
                        {
                            correctWireNum = 1;
                            Debug.Log("5-4 �߶�� �ϴ� ���̾� : " + correctWireNum + "��");
                        }
                    }
                }
                break;
        }
        
    }

    List<string> GetRandomColors(List<string> colors, int count)
    {
        List<string> rColors = new List<string>();

        for (int i = 0; i < count; i++)
        {
            int rIdx = Random.Range(0, colors.Count);
            rColors.Add(colors[rIdx]);
        }

        return rColors;
    }
    
    public void CutWire(int idx)
    {
        if (!isDefused && GameManager.Instance.isGameStart && !GameManager.Instance.isGameOver)
        {
            if (idx == GameManager.Instance.incorrectCnt)
            {
                isDefused = true;
                GameManager.Instance.defuesedCnt++;
                Debug.Log("���̾� ��� ���� ����");
                if (GameManager.Instance.defuesedCnt == GameManager.Instance.totalModuleCnt)
                {
                    GameManager.Instance.GameClear();
                }
            }
            else
            {
                GameManager.Instance.incorrectCnt++;
                Debug.Log("���� �� : " + GameManager.Instance.incorrectCnt);
                if (GameManager.Instance.incorrectCnt >= 3)
                {
                    bomb.Fail();
                    Debug.Log("Game Over!!!");
                }
            }
        }        
    }
}
