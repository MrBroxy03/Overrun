using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundHitbox : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit by ground pound!");
        }
    }
}
