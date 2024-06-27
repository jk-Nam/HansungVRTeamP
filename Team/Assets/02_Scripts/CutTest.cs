using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutTest : MonoBehaviour
{
    public List<Button> cutButtons = new List<Button>();

    WiresModule wire;
    public Dictionary<Button, Transform> buttonParentTransforms = new Dictionary<Button, Transform>();
    private Dictionary<Button, Vector3> originalScales = new Dictionary<Button, Vector3>();

    private void Start()
    {
        //SetUpButton(cut1);
        //SetUpButton(cut2);
        //SetUpButton(cut3);
        //SetUpButton(cut4);
        //SetUpButton(cut5);

        OnClickSetUp();
    }


    public void OnClickSetUp()
    {
        wire = GameObject.Find("WireTest").GetComponentInChildren<WiresModule>();

        cutButtons[0].onClick.AddListener(() =>
        {
            wire.CutWire(1);
            cutButtons[0].interactable = false;
        });
        cutButtons[1].onClick.AddListener(() =>
        {
            wire.CutWire(2);
            cutButtons[1].interactable = false;
        });
        cutButtons[2].onClick.AddListener(() =>
        {
            wire.CutWire(3);
            cutButtons[2].interactable = false;
        });
        cutButtons[3].onClick.AddListener(() =>
        {
            wire.CutWire(4);
            cutButtons[3].interactable = false;
        });
        cutButtons[4].onClick.AddListener(() =>
        {
            wire.CutWire(5);
            cutButtons[4].interactable = false;
        });
    }
}
