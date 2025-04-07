using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskMeter : MonoBehaviour
{
    public Slider maskMeter;
    public static int maxMeter = 300;
    public static int meter;
    // Start is called before the first frame update
    void Start()
    {
        meter = maxMeter;
        maskMeter.maxValue = maxMeter;
        maskMeter.value = meter;      
    }

    // Update is called once per frame
    void Update()
    {
        maskMeter.value = meter;
        if (Input.GetKey(KeyCode.B))
        {
            meter = 300;
        }
    }
}
