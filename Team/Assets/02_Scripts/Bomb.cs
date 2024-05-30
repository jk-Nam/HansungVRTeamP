using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject module1;
    public List<GameObject> modules;
    public GameObject expEffect;

    public Transform[] modulesTr;

    void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject module = Instantiate(module1, modulesTr[i].transform.position, modulesTr[i].transform.rotation);
        //    module.name = modulesTr[i].name;
        //    module.transform.SetParent(modulesTr[i].transform);
        //}

        GameObject module = Instantiate(modules[0], modulesTr[0].transform.position, modulesTr[0].transform.rotation);
        module.transform.SetParent(modulesTr[0]);
        module.transform.localScale = new Vector3(0.2f, 0.8f, 0.8f);
        module.name = "WireModule";
    }

    public void Fail()
    {
        //게임오버
        GameManager.Instance.GameOver();
        //폭발 이펙트 생성
        //Instantiate(bomb.expEffect, bomb.transform.position, bomb.transform.rotation);
    }
}
