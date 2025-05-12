using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RailManager : MonoBehaviour
{
    public GameObject rail;
    public GameObject player;

    MeshRenderer railRenderer;
    public SplineContainer splineContainer;

    void Start()
    {
        splineContainer = rail.GetComponent<SplineContainer>();
        railRenderer = rail.GetComponent<MeshRenderer>();
        railRenderer.material.color = Color.black;
    }

    void Update()
    {
        if (player.gameObject.GetComponent<SplineAnimate>().isActiveAndEnabled == true)
        {
            railRenderer.material.color = Color.yellow;
        }
        else{
            railRenderer.material.color = Color.black;
        }
    }


}
