using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings.Frustum;

public class MaskMeter : MonoBehaviour
{
    public Slider maskMeter;
    public static float maxMeter = 300f;
    public static float meter;
    private bool infiniteMode = false;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        meter = maxMeter;
        maskMeter.maxValue = maxMeter;
        maskMeter.value = meter;
        ogColor = maskMeter.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color;
    }
    private bool pressingKey = false;
    // Update is called once per frame
    void Update()
    {
        maskMeter.value = meter;
        if (Input.GetKeyDown(KeyCode.B) && !pressingKey)
        {
            pressingKey = true;
            infiniteMode = !infiniteMode;
            if (infiniteMode)
            {
                maskMeter.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                maskMeter.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = ogColor;
            }
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            pressingKey = false;
        }
        if (infiniteMode)
        {
            meter = maxMeter;
        }
    }
}
