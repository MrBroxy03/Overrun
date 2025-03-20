using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3;
    public float hitCountdown = 0f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitCountdown == 0)
        {
            health = health - 1;
            hitCountdown = 1f;
        }
    }

    void Update()
    {
        if (hitCountdown > 0)
        {
            hitCountdown -= Time.deltaTime;
        } else {
            hitCountdown = 0;
        }

        if (health == 0){
            SceneManager.LoadScene (0);
        }
    }
}
