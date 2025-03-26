using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 500.0f;
    public float jumpForce = 3f;

    private bool isOnGround = true;
    private bool jumping = false;

    private Rigidbody rigidB;
    private BoxCollider groundCollider;
    private bool space;
    private bool ledgeGrabbed;

    private float moveFoward;
    private float moveSideways;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        groundCollider = this.transform.GetComponent<BoxCollider>();
    }
    private void LedgeGrab()
    {
        Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit bodyCheck, 1f);
        Physics.Raycast(this.transform.position + new Vector3(0, 1, 0), this.transform.TransformDirection(Vector3.forward), out RaycastHit headCheck, 1f);
        if (bodyCheck.collider != null && headCheck.collider == null){
            Debug.Log("Ledge Grab");
            jumping = false;
            ledgeGrabbed = true;
            this.transform.position = this.transform.position + new Vector3(0, 1f,-(1-bodyCheck.distance));
            rigidB.AddForce(transform.forward * (1f), ForceMode.Impulse);
        }
        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 1f, Color.blue);
        Debug.DrawRay(this.transform.position + new Vector3(0,1,0), this.transform.TransformDirection(Vector3.forward)*1f,Color.red);
    }
    // Update is called once per frame
    private bool CheckGround()
    {
       if (rigidB.velocity.y > 0f) return false;
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.red);
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.red);
        return Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck2, 1f);
    }

    void Jump()
    {
        jumping = true;
        Debug.Log("Jumping");
        rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void Update()
    {

        isOnGround = CheckGround();
        if (isOnGround)
        {
            ledgeGrabbed = false;
            Debug.Log("OnGround");
            jumping = false;
        }
        moveFoward = 0;
        moveSideways = 0;

        Debug.Log(rigidB.velocity);
        if (!ledgeGrabbed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit fowardCheck, 0.75f);
                if (fowardCheck.collider == null) {
                    moveFoward = movementSpeed;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveSideways = -movementSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveFoward = -movementSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveSideways = movementSpeed;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isOnGround)
                {
                    LedgeGrab();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumping)
            {
                Jump();
            }
        }
        if (ledgeGrabbed)
        {
            rigidB.velocity = (this.transform.forward * moveFoward) + (this.transform.right * moveSideways) + (this.transform.up * rigidB.velocity.y);
        }
        rigidB.AddForce((-transform.up * 6.5f), ForceMode.Force);
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.blue);
    }
        
}
