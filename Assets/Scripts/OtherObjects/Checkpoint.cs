using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private int currentCheckpoint = 0;
    [SerializeField] public GameObject[] checkpoints;
    int health = PlayerHealth.health;

    void Start()
    {
        currentCheckpoint = 0;
        this.transform.position = checkpoints[0].transform.position;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint") && collision.gameObject.GetComponent<Collider>().enabled)
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            currentCheckpoint = Mathf.Clamp(currentCheckpoint+1,currentCheckpoint, checkpoints.Length-1);
        }
    }

    void Update()
    {
        if (PlayerHealth.health == 0)
        {
            Debug.Log("Bro was defeated");
            this.transform.position = checkpoints[currentCheckpoint].transform.position;
            PlayerHealth.health = 3;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && currentCheckpoint > 0)
        {
            
            this.transform.position = checkpoints[currentCheckpoint-1].transform.position;
            currentCheckpoint -= 1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && currentCheckpoint < checkpoints.Length-1)
        {
            checkpoints[currentCheckpoint + 1].gameObject.GetComponent<Collider>().enabled = false;
            this.transform.position = checkpoints[currentCheckpoint + 1].transform.position;
            currentCheckpoint += 1;
        }
    }
}
