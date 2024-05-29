using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject wireModule;

    public Transform[] modulesTr;

    int num = 0;

    void Start()
    {
        Instantiate(wireModule, modulesTr[0].transform.position, modulesTr[0].transform.rotation);
        
    }

}
