using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject openUI;
    public GameObject closeUI;
    public void OpenMenu()
    {
        openUI.SetActive(true);
        closeUI.SetActive(false);
    }
}
