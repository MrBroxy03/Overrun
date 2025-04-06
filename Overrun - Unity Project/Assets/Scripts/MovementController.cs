using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 500.0f;
    public float jumpForce = 3f;

    private float ledgeGrabCD = 0f;

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
       if (bodyCheck.collider != null && headCheck.collider == null && ledgeGrabCD == 0){
            Debug.Log("Ledge Grabed");
            ledgeGrabbed = true;
            rigidB.AddForce(transform.up * (1f), ForceMode.Impulse);
            rigidB.AddForce(transform.forward * (1f), ForceMode.Impulse);
            ledgeGrabCD = 1f;
        }
        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 1f, Color.blue);
        Debug.DrawRay(this.transform.position + new Vector3(0,1,0), this.transform.TransformDirection(Vector3.forward)*1f,Color.red);
    }
    // Update is called once per frame
    private bool CheckGround()
    {
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.red);
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.red);
       Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck2, 1.2f);
        Debug.Log(bodyCheck2);
        return Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck25, 1.2f); ;
    }

    void Jump()
    {
        if (!ledgeGrabbed && CheckGround())
        {
            jumping = true;
            Debug.Log("Jumping");
            rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else if(ledgeGrabbed && ledgeGrabCD == 0)
        {
            Debug.Log("LedgeGrabbed");
            jumping = false;
            ledgeGrabbed = false;
            ledgeGrabCD = 1f;
            rigidB.AddForce(transform.up * jumpForce/10, ForceMode.Impulse);
        }
    }
        void Update()
        {
            moveFoward = 0;
            moveSideways = 0;

            Debug.Log(ledgeGrabbed);
            //Debug.Log(rigidB.velocity);
            if (!ledgeGrabbed)
            {
                isOnGround = CheckGround();

                if (isOnGround)
                {
                    ledgeGrabbed = false;
                    Debug.Log("OnGround");
                    jumping = false;
                }
            if (Input.GetKey(KeyCode.W))
                {
                    Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit fowardCheck, 0.75f);
                    if (fowardCheck.collider == null)
                    {
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
            }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isOnGround)
            {
                LedgeGrab();
            }
            else
            {
                Jump();
            }
        }

            if (!ledgeGrabbed)
            {
                rigidB.AddForce((-transform.up * 6.5f), ForceMode.Force);
                rigidB.velocity = (this.transform.forward * moveFoward) + (this.transform.right * moveSideways) + (this.transform.up * rigidB.velocity.y);
            }
            else
            {
                rigidB.velocity = Vector3.zero;
            }
           
            Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.blue);
            if (ledgeGrabCD > 0)
            {
                ledgeGrabCD -= Time.deltaTime;
            }
            else
            {
                ledgeGrabCD = 0;
            }
        }

        
}
