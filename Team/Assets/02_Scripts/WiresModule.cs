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
                GetRandomColors(wireColor, 3);
                Debug.Log(wireColor);
                break;
            case 4:
                GetRandomColors(wireColor, 4);
                Debug.Log(GetRandomColors(wireColor, 4));
                break;
            case 5:
                GetRandomColors(wireColor, 5);
                Debug.Log(GetRandomColors(wireColor, 5));
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
    
}
