using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutTest : MonoBehaviour
{
    public Button cut1;
    public Button cut2;
    public Button cut3;
    public Button cut4;
    public Button cut5;

    WiresModule wire;

    private void Start()
    {
        
    }

    public void OnClickSetUp()
    {
        wire = GameObject.Find("WireModule").GetComponent<WiresModule>();

        cut1.onClick.AddListener(() => wire.CutWire(1));
        cut2.onClick.AddListener(() => wire.CutWire(2));
        cut3.onClick.AddListener(() => wire.CutWire(3));
        cut4.onClick.AddListener(() => wire.CutWire(4));
        cut5.onClick.AddListener(() => wire.CutWire(5));

    }


}
