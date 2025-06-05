using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TitleScreen : MonoBehaviour
{
    public Animator animHermes;
    public Animator animEffect;
    public Animator intro;
    public GameObject introgmobj;
    public GameObject blackScreen;
    public GameObject particle;
    public GameObject background;
    public Canvas Canvas;
    public static bool introplayed=false;
    public float changeScene = 0f;
    private float timepast = 0f;
    void Start()
    {
        Debug.Log("Kaboom, There goes your tower, watch it crumble, feel the power");
        if (!introplayed)
        {
            introgmobj.SetActive(true);
            intro.Play("Intro");
        }
        else
        {
            background.SetActive(true);
            Destroy(particle);
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            background.SetActive(true);
            Debug.Log("Kaboom");
            intro.enabled = false;
            Destroy(introgmobj);
            Destroy(particle);
        }
           
        blackScreen.SetActive(false);
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (introgmobj != null)
        {
            if (timepast > 4f && particle != null)
            {
                background.SetActive(true);
                Destroy(particle);
                Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            if (timepast > 5f && introgmobj != null)
            {
                Debug.Log("Kaboom");
                intro.enabled = false;
                Destroy(introgmobj);
                Destroy(particle);
                introplayed = true;
            }

            timepast += dt;
        }

        if (changeScene > 0)
        {
            changeScene += dt;
        }

        if (changeScene >= 2)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void GoToLevel()
    {
        animHermes.SetTrigger("go2Level");
        Debug.Log(animHermes);
        animEffect.SetTrigger("FadeIn");
        blackScreen.SetActive(true);
        changeScene = 0.1f;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
