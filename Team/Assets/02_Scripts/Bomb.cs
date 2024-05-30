using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject module1;
    public GameObject wireModule;

    public Transform[] modulesTr;

    void Start()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject module = Instantiate(module1, modulesTr[i].transform.position, modulesTr[i].transform.rotation);
        //    module.name = modulesTr[i].name;
        //    module.transform.SetParent(modulesTr[i].transform);
        //}

        GameObject module = Instantiate(wireModule, modulesTr[0].transform.position, modulesTr[0].transform.rotation);
        module.transform.SetParent(modulesTr[0]);
        module.transform.localScale = new Vector3(0.2f, 0.8f, 0.8f);
    }

}
