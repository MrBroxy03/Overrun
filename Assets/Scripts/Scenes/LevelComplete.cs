using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{

    public float changeScene = 0f;
    public Animator animEffect;
    public GameObject blackScreen;

    void Start()
    {
        if (animEffect == null)
        {
            animEffect = GetComponent<Animator>();
        }

        blackScreen.SetActive(false);
    }

    void Update()
    {
        if (changeScene >= 3)
        {
            animEffect.SetTrigger("fadeIn");
            blackScreen.SetActive(true);
        }

        if (changeScene >= 5)
        {
            SceneManager.LoadScene(0);
        } else {
            changeScene += Time.deltaTime;
        }
    }
}
