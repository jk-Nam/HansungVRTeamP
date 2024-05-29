using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject module1;

    public Transform[] modulesTr;


    void Start()
    {
        for (int i = 0; i < modulesTr.Length; i++)
        {
            GameObject module =  Instantiate(module1, modulesTr[i].transform.position, modulesTr[i].transform.rotation);
            module.name = modulesTr[i].name;
            module.transform.SetParent(modulesTr[i].transform);
        }
        
    }

}
