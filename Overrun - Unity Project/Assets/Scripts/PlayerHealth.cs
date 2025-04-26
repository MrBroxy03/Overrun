using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 3;
    public float hitCountdown = 0f;

    public GameObject HP3;
    public GameObject HP2;
    public GameObject HP1;

    private PlayerCombat plyCombat;
    private HomingAttack hAttack;
    private MovementController boosting;

    public CameraShake cameraShake;
    
    private void Start()
    {
        plyCombat = gameObject.GetComponent<PlayerCombat>();
        hAttack = gameObject.GetComponent<HomingAttack>();
        boosting = gameObject.GetComponent<MovementController>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitCountdown == 0 && !plyCombat.isGroundPound && !hAttack.homingAttack && !boosting.isBoosting)
        {
            health = health - 1;
            hitCountdown = 1f;
            StartCoroutine(cameraShake.Shaking(.50f, .15f));
            ChangeUI();
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
            HP3.SetActive(false);
        }

        if (health == 1)
        {
            HP2.SetActive(false);
        }

        if (health == 0)
        {
            HP1.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            StartCoroutine(cameraShake.Shaking(.20f, .4f));
        }
        if (health == 3 && HP1 != null)
        {
            HP3.SetActive(true);
            HP2.SetActive(true);
            HP1.SetActive(true);
        }

        if (hitCountdown > 0)
        {
            hitCountdown -= Time.deltaTime;
        } else {
            hitCountdown = 0;
        }

    }
}