using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Surfaces;
using Oculus.Interaction;

public class MaxRayOption : MonoBehaviour
{
    public RayInteractor leftRay;
    public RayInteractor rightRay;

    public float rayDefault = 0.1f;
    public float rayMax = 20.0f;

    public void RayDefault()
    {
        leftRay.MaxRayLength = rightRay.MaxRayLength = rayDefault;
    }

    public void RayMax()
    {
        leftRay.MaxRayLength = rightRay.MaxRayLength = rayMax;
    }
}
