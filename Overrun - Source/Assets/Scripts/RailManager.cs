using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RailManager : MonoBehaviour
{
    public GameObject rail;
    public SplineContainer splineContainer;

    void Start()
    {
        splineContainer = rail.GetComponent<SplineContainer>();
    }

}
