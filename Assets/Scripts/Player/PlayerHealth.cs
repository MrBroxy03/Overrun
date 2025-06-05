using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 3;
    private float hitCountdown = 0f;

    public GameObject hP3;
    public GameObject hP2;
    public GameObject hP1;

    private PlayerCombat plyCombat;
    private HomingAttack hAttack;
    private MovementController boosting;

    private bool godMode = false;

    public CameraShake cameraShake;

    private Image health1Image;
    private Image health2Image;
    private Image health3Image;
    private void Start()
    {
        health1Image = hP1.GetComponent<Image>();
        health2Image = hP2.GetComponent<Image>();
        health3Image = hP3.GetComponent<Image>();
        plyCombat = gameObject.GetComponent<PlayerCombat>();
        hAttack = gameObject.GetComponent<HomingAttack>();
        boosting = gameObject.GetComponent<MovementController>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (plyCombat.groundPoundAttack == 0 && !hAttack.homingAttack && !boosting.isBoosting)
        {
            if (collision.gameObject.CompareTag("Enemy") && hitCountdown == 0 && !godMode)
            {
                health = health - 1;
                hitCountdown = 1f;
                StartCoroutine(cameraShake.Shaking(.50f, .15f));
                ChangeUI();
            }
        }
        

        if (collision.gameObject.CompareTag("Water"))
        {
            health = 0;
        }
    }

    public void ChangeUI()
    {
        if (health == 2)
        {
            hP3.SetActive(false);
        }

        if (health == 1)
        {
            hP2.SetActive(false);
        }

        if (health == 0)
        {
            hP1.SetActive(false);
            MaskMeter.meter = 300;
        }
    }
    private bool pressingKey = false;
    void Update()
    {
        if (!MenuSystem.paused)
        {
            if (Input.GetKeyDown(KeyCode.H) && !pressingKey)
            {
                pressingKey = true;
                godMode = !godMode;
                if (godMode)
                {
                    health1Image.color = Color.yellow;
                    health2Image.color = Color.yellow;
                    health3Image.color = Color.yellow;
                }
                else
                {
                    health1Image.color = Color.red;
                    health2Image.color = Color.red;
                    health3Image.color = Color.red;
                }
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                pressingKey = false;
            }

            if (health == 3 && hP1 != null)
            {
                hP3.SetActive(true);
                hP2.SetActive(true);
                hP1.SetActive(true);
            }

            if (hitCountdown > 0)
            {
                hitCountdown -= Time.deltaTime;
            }
            else
            {
                hitCountdown = 0;
            }
        }

    }
}