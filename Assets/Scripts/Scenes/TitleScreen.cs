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

    public float changeScene = 0f;
    private float timepast = 0f;
    void Start()
    {
        if (animHermes == null)
        {
            animHermes = GetComponent<Animator>();
            animEffect = GetComponent<Animator>();
        }

        introgmobj.SetActive(true);
        intro.Play("Intro");

        blackScreen.SetActive(false);
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (timepast > 4f && particle != null)
        {
            background.SetActive(true);
            Destroy(particle);
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        if (timepast > 5f && introgmobj != null)
        {
            background.SetActive(true);
            Debug.Log("Kaboom");
            intro.enabled = false;
            Destroy(introgmobj);
            Destroy(particle);

        }
       
        timepast += dt;
        

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
        animHermes.SetBool("go2Level", true);
        animEffect.SetTrigger("fadeIn");
        blackScreen.SetActive(true);
        changeScene = 0.1f;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
