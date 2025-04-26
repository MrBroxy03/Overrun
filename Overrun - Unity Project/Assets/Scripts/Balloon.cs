using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    private Collider BalloonsCollider;
    public float JumpForce = 8f;
    
    void Start()
    {
        BalloonsCollider = gameObject.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kaddosh");
        Debug.Log(collision.gameObject.tag);
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (BalloonsCollider != null && collision.gameObject.CompareTag("Player") && rb != null)
        {
            Debug.Log("Kadooshed");
            rb.AddForce((transform.up * JumpForce), ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
