using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    

    public GameObject module1;
    public GameObject expEffect;

    public List<GameObject> modules;
    public List<Transform> modulesTr;

    void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject module = Instantiate(module1, modulesTr[i].transform.position, modulesTr[i].transform.rotation);
        //    module.name = modulesTr[i].name;
        //    module.transform.SetParent(modulesTr[i].transform);
        //}
        

        for (int i = 0; i < modules.Count + 1 ; i++)
        {
            int rModuleNum = Random.Range(0, modules.Count);
            int rPos = Random.Range(0, modulesTr.Count);
            GameObject module = Instantiate(modules[rModuleNum], modulesTr[rPos].transform.position, modulesTr[rPos].transform.rotation);
            module.transform.SetParent(modulesTr[rPos]);
            module.transform.localScale = new Vector3(0.2f, 0.8f, 0.8f);
            module.name = module.name.Replace("(Clone)", "");
            //module.name = module.GetComponent<>().moduleType.ToString() + "Module";
            //switch(rModuleNum)
            //{
            //    case 0:
            //        module.name = "WireModule";            
            //        break;
            //    case 1:
            //        module.name = "KeypadModule";
            //        break;
            //}

            modulesTr.RemoveAt(rPos);
            modules.RemoveAt(rModuleNum);
        }
        
    }

    public void Fail()
    {
        //게임오버
        GameManager.Instance.GameOver();
        //폭발 이펙트 생성
        //Instantiate(bomb.expEffect, bomb.transform.position, bomb.transform.rotation);
    }
}
