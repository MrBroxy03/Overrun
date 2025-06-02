using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Animator animHermes;
    public Animator animEffect;
    public GameObject blackScreen;
    
    public float changeScene = 0f;

    void Start()
    {
        if (animHermes == null)
        {
            animHermes = GetComponent<Animator>();
            animEffect = GetComponent<Animator>();
        }

        blackScreen.SetActive(false);
    }

    void Update()
    {
        if (changeScene > 0)
        {
            changeScene += Time.deltaTime;
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
