using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float jumpForce = 3f;
    private bool IsOnGround = true;

    public Rigidbody rigidB;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && IsOnGround)
        {
            IsOnGround = false;
            rigidB.AddForce(transform.up*jumpForce, ForceMode.Impulse);
        }
        rigidB.AddForce(-transform.up * 6.5f, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // if (collision.gameObject.tag == "Ground")
       // {
            IsOnGround = true;
      //  }
    }
}
