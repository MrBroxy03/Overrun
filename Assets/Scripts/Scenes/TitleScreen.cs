using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }
    public void GoToLevel()
    {
        //SceneManager.LoadScene(1);
        anim.SetBool("go2Level", true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
