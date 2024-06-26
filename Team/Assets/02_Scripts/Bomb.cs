using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject expEffect;


    public List<GameObject> modules;
    public List<Transform> modulesTr;


    void Start()
    {
        //모듈 랜덤 생성
        for (int i = 0; i < modules.Count + 1 ; i++)
        {
            int rModuleNum = Random.Range(0, modules.Count);
            int rPos = Random.Range(0, modulesTr.Count);
            GameObject module = Instantiate(modules[rModuleNum], modulesTr[rPos].transform.position, modulesTr[rPos].transform.rotation);
            module.transform.SetParent(modulesTr[rPos]);
            //module.transform.localScale = new Vector3(0.2f, 0.8f, 0.8f);
            module.name = module.name.Replace("(Clone)", "");
            //module.name = module.GetComponent<BombModule>().moduleType.ToString() + "Module";
            //switch (rModuleNum)
            //{
            //    case 0:
            //        module.name = "WireModule";
            //        break;
            //    case 1:
            //        module.name = "KeypadModule";
            //        break;
            //    case 2:
            //        module.name = "MazeModule";
            //        break;
            //}

            modulesTr.RemoveAt(rPos);
            modules.RemoveAt(rModuleNum);
        }
        
    }

    public void Fail()
    {
        
        GameManager.Instance.GameOver();
        //실패 시 이팩트 생성
        //Instantiate(bomb.expEffect, bomb.transform.position, bomb.transform.rotation);
    }
}
