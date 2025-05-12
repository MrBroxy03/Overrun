using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public float option = 0;
    public float optionTimeout = 0;

    public GameObject start;
    public GameObject exit;

    public Texture startGame;
    public Texture startGameSelected;

    public Texture exitGame;
    public Texture exitGameSelected;

    void Start()
    {
        ChangeOption();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W) && optionTimeout == 0)
        {
            option -= 1;
            optionTimeout = 0.2f;
            ChangeOption();
        }

        if (Input.GetKey(KeyCode.S) && optionTimeout == 0)
        {
            option += 1;
            optionTimeout = 0.2f;
            ChangeOption();
        }

        if (Input.GetKeyDown(KeyCode.Return) && option == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Return) && option == 1)
        {
            Application.Quit();
        }

        if (optionTimeout > 0)
        {
            optionTimeout -= Time.deltaTime;
        }

        if (optionTimeout < 0)
        {
            optionTimeout = 0;
        }
    }

    void ChangeOption()
    {
        if (option == -1)
        {
            option = 1;
        }
        
        if (option == 2)
        {
            option = 0;
        }

        if (option == 0)
        {
            start.gameObject.GetComponent<RawImage>().texture = startGameSelected;
            exit.gameObject.GetComponent<RawImage>().texture = exitGame;
        }

        if (option == 1)
        {
            start.gameObject.GetComponent<RawImage>().texture = startGame;
            exit.gameObject.GetComponent<RawImage>().texture = exitGameSelected;
        }

    }
}
