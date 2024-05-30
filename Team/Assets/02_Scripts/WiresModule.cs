using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresModule : BombModule
{
    public WiresModule()
    {
        moduleType = BombMoudleType.Wire;
    }

    public List<string> wireColor = new List<string> { "Red", "Blue", "Black", "White", "Yellow"};

    int wireCnt;
    public int correctWireNum;

    private void Start()
    {
        InitiallizeModule();
        DefuseModule();
    }

    public override void InitiallizeModule()
    {
        incorrectCnt = 0;
        //wireCnt = Random.Range(3, 6);
        wireCnt = 5;
        Debug.Log("Wire의 수는 " + wireCnt + "개 입니다.");
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
                        if(wireColor.FindAll(w => w == "Blue").Count == 1)
                        {
                            correctWireNum = 1;
                            Debug.Log("4-3 잘라야 하는 와이어 : " + correctWireNum + "번");
                        }
                        else
                        {
                            if (wireColor.FindAll(w => w == "Yellow").Count >= 2)
                            {
                                correctWireNum = wireColor.FindLastIndex(str => str == "Yellow") + 1;
                                Debug.Log("4-4 잘라야 하는 와이어 : " + correctWireNum + "번");
                            }
                            correctWireNum = 2;
                            Debug.Log("4-5 잘라야 하는 와이어 : " + correctWireNum + "번");
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

    public override void Fail()
    {
        
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
        if (idx == incorrectCnt)
        {
            isDefused = true;
        }
        else
        {
            incorrectCnt++;
            if (incorrectCnt >= 3)
            {
                Fail();
            }
        }
    }
}
