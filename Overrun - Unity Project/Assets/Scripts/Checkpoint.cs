using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject checkpoint;
    public GameObject startingPosition;
    int health = PlayerHealth.health;

    void Start()
    {
        this.transform.position = startingPosition.transform.position;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpoint = collision.gameObject;
            checkpoint.GetComponent<Collider>().enabled = false;
            Debug.Log("Got a checkpoint!");
        }
    }

    void Update()
    {
        if (PlayerHealth.health == 0)
        {
            if (checkpoint == null)
            {
                this.transform.position = startingPosition.transform.position;
            } else {
                this.transform.position = checkpoint.transform.position;
            }
            PlayerHealth.health = 3;
        }
    }
}
