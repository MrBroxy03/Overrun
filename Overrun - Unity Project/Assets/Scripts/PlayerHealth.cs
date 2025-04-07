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

    public float groundPoundAttack = MovementController.groundPoundAttack;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitCountdown == 0 && MovementController.groundPoundAttack == 0)
        {
            health = health - 1;
            hitCountdown = 1f;
            ChangeUI();
        }
    }
    
    void ChangeUI()
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
        Debug.Log(MovementController.groundPoundAttack);
        if (health == 3)
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
