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
    int correctWireNum;

    private void Start()
    {
        InitiallizeModule();
        DefuseModule();
    }

    public override void InitiallizeModule()
    {
        incorrectCnt = 0;
        wireCnt = Random.Range(3, 6);
        Debug.Log("Wire의 수는 " + wireCnt + "개 입니다.");
    }

    public override void DefuseModule()
    {
        switch (wireCnt)
        {
            case 3:
                List<string> ThreeColor = GetRandomColors(wireColor, 3);
                wireColor = ThreeColor;
                if (wireColor.Contains("Red"))
                {
                    if (wireColor[2] == "White")
                    {
                        correctWireNum = 2;
                    }
                    else
                    {
                        if (wireColor.FindAll(w => w == "Blue").Count >= 2)
                        {
                            //빨 파 파, 파 빨 파, 파 파 빨
                            if (wireColor[2] == "Blue")
                            {
                                correctWireNum = 2;
                            }
                            else
                            {
                                correctWireNum = 1;
                            }
                        }
                        else
                        {
                            correctWireNum = 2;
                        }
                    }
                }
                else
                {
                    correctWireNum = 1;
                }
                    break;
            case 4:
                List<string> fourColor = GetRandomColors(wireColor, 4);
                wireColor = fourColor;
                if (wireColor.FindAll(w => w == "Red").Count >= 2)
                {
                    correctWireNum = wireColor.FindLastIndex(str => str == "Red");
                }
                else
                {
                    if (wireColor[3] == "Yellow" && !wireColor.Contains("Red"))
                    {
                        incorrectCnt = 1;
                    }
                    else
                    {
                        if(wireColor.FindAll(w => w == "Blue").Count == 1)
                        {
                            incorrectCnt = 1;
                        }
                        else
                        {
                            incorrectCnt = 2;
                        }
                    }
                }
                break;
            case 5:
                List<string> fiveColor = GetRandomColors(wireColor, 5);
                wireColor = fiveColor;

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
