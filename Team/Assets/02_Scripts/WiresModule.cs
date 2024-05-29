using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresModule : BombModule
{
    public WiresModule()
    {
        moduleType = BombMoudleType.Wire;
    }

    public List<string> wireColor;

    int wireCnt;

    private void Start()
    {
        InitiallizeModule();
    }

    public override void InitiallizeModule()
    {
        incorrectCnt = 0;
        wireCnt = Random.Range(3, 6);
        Debug.Log(wireCnt);
    }

    public override void DefuseModule()
    {
        switch (wireCnt)
        {
            case 3:
                
                break;
            case 4:

                break;
            case 5:

                break;
        }
    }

    public override void Fail()
    {
        
    }

    
}
