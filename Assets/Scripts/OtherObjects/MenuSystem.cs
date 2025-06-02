using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    public GameObject settingsUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(false);
    }
    public void OpenSettings(bool visibility)
    {
        settingsUI.SetActive(visibility);
        if (visibility)
        {
            pauseUI.SetActive(false);
        }
        else
        {
            pauseUI.SetActive(true);
        }
    }
    public void GoBackToTitle()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseFunc()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UnpauseFunc()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            paused = !paused;
            if (paused)
            {
                PauseFunc();
            }
            else
            {
                UnpauseFunc();
            }
        }
    }
}
