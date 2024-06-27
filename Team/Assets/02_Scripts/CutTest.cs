using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OVR;

public class CutTest : MonoBehaviour
{
    public Button cut1;
    public Button cut2;
    public Button cut3;
    public Button cut4;
    public Button cut5;

    WiresModule wire;
    public Dictionary<Button, Transform> buttonParentTransforms = new Dictionary<Button, Transform>();
    private Dictionary<Button, Vector3> originalScales = new Dictionary<Button, Vector3>();

    public List<GraphicRaycaster> raycasters = new List<GraphicRaycaster>();
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private void Start()
    {
        //eventSystem = EventSystem.current; if (eventSystem == null)
        //{
        //    Debug.LogError("EventSystem not found in the scene.");
        //    return;
        //}

        //GraphicRaycaster[] allRaycasters = GetComponentsInChildren<GraphicRaycaster>();
        //raycasters.AddRange(allRaycasters);

        //if (raycasters.Count == 0)
        //{
        //    Debug.LogError("No GraphicRaycaster found in children of this GameObject.");
        //    return;
        //}

        //SetUpButton(cut1);
        //SetUpButton(cut2);
        //SetUpButton(cut3);
        //SetUpButton(cut4);
        //SetUpButton(cut5);
    }

    private void Update()
    {
        //CheckUIRaycast();
    }

    private void CheckUIRaycast()
    {
        if (eventSystem == null || raycasters.Count == 0)
        {
            Debug.LogError("EventSystem or GraphicRaycaster is null. Cannot perform UI raycast.");
            return;
        }
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }

        pointerEventData.position = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTouch);

        // Raycast using the graphic raycaster and pointer event data
        List<RaycastResult> results = new List<RaycastResult>();
        foreach (GraphicRaycaster raycaster in raycasters)
        {
            raycaster.Raycast(pointerEventData, results);

            // Process the results
            bool hitButton = false;
            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null && buttonParentTransforms.ContainsKey(button))
                {
                    Debug.Log($"Raycast hit button: {button.name}");
                    OnPointerEnter(button);
                    hitButton = true;
                    break;
                }
            }

            if (hitButton)
            {
                break; // Exit the loop if a button was hit
            }
        }

        if (results.Count == 0)
        {
            ResetScales();
        }
    }

    private void ResetScales()
    {
        foreach (var kvp in buttonParentTransforms)
        {
            kvp.Value.localScale = originalScales[kvp.Key];
        }
    }

    private void SetUpButton(Button button)
    {
        Transform parentTransform = button.transform.parent.parent;
        buttonParentTransforms[button] = parentTransform;
        originalScales[button] = parentTransform.localScale;
    }

    private void OnPointerEnter(Button button)
    {
        foreach (var kvp in buttonParentTransforms)
        {
            if (kvp.Key == button)
            {
                kvp.Value.localScale = new Vector3(1.0f, 2.0f, 1.0f);
                Debug.Log($"Scaling parent's parent of {button.name} to new scale");
            }
            else
            {
                kvp.Value.localScale = originalScales[kvp.Key];
            }
        }
    }

    public void OnClickSetUp()
    {
        wire = GameObject.Find("WireTest").GetComponentInChildren<WiresModule>();

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
