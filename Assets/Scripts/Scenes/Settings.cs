using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{

    public GameObject lowBtn, highBtn, background, handle;
    public Slider volumeSlider;
    public static int cameraDistance = 800;
    public static float gamesvolume = 0.8f;
    [SerializeField] private TMP_Dropdown resolutionlist;
    private Resolution[] resolutions; 
    private List<Resolution> selectedresolution;
    private int currentResolutionIndex = 0;
    private RefreshRate currentRefreshRate;

    public void Start()
    {
        SetFullscreen(Screen.fullScreen);

        resolutions = Screen.resolutions;
        selectedresolution = new List<Resolution>();

        resolutionlist.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRateRatio;
        
        for (int i = 0; i < resolutions.Length; i++) { 

            if (resolutions[i].refreshRateRatio.numerator == currentRefreshRate.numerator)
            {
                selectedresolution.Add(resolutions[i]);
            }
        }

        volumeSlider.value = gamesvolume;

        List<string> resoptions = new List<string>();

        for (int i = 0; i < selectedresolution.Count; i++) 
        { 
            string option = selectedresolution[i].width + "x" + selectedresolution[i].height;
            resoptions.Add(option);
            if (selectedresolution[i].width == Screen.width && selectedresolution[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionlist.AddOptions(resoptions);
        resolutionlist.value = currentResolutionIndex;
        resolutionlist.RefreshShownValue();

    }
    public void VolumeChange()
    {
        
        gamesvolume = volumeSlider.value;
    }
    public void ResolutionChange(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void SetFullscreen(bool boolean)
    {
        if (boolean)
        {
            handle.GetComponent<RectTransform>().localPosition = new Vector3(19, 0, 0);
            background.GetComponent<Image>().color = new Color(33f / 255f, 167f / 255f, (188f / 255f), 1);
        }
        else
        {
            handle.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            background.GetComponent<Image>().color = new Color((131f / 255f), (131f / 255f), (131f / 255f), 1);
        }
        Screen.fullScreen = boolean;
    }
    public void ToggleFullscreen()
    {
        bool abool = !Screen.fullScreen;
        SetFullscreen(abool);
    }

    public void GraphicChange(int graphic)
    {
        Debug.Log(graphic);
        if (graphic == 1)
        {
            cameraDistance = 200;
           
            lowBtn.SetActive(true);
            highBtn.SetActive(false);
        }
        else
        {
            cameraDistance = 800;
            lowBtn.SetActive(false);
            highBtn.SetActive(true);
        }
    }
}
