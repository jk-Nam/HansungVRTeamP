using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;

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
        wire = GameObject.Find("WireTest").GetComponentInChildren<WiresModule>();
    }

    public void OnClickSetUp()
    {
        cut1.onClick.AddListener(() => {
            wire.CutWire(1);
            cut1.interactable = false;
        });
        cut2.onClick.AddListener(() => {
            wire.CutWire(2);
            cut2.interactable = false;
        });
        cut3.onClick.AddListener(() => {
            wire.CutWire(3);
            cut3.interactable = false;
        });
        cut4.onClick.AddListener(() => {
            wire.CutWire(4);
            cut4.interactable = false;
        });
        cut5.onClick.AddListener(() => {
            wire.CutWire(5);
            cut5.interactable = false;
        });
    }

}
