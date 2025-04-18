using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 500.0f;
    public float jumpForce = 3f;

    private float jumpCD = 0f;

    private bool isOnGround = true;
    private bool jumping = false;
    public bool sliding = false;

    private Rigidbody rigidB;
    private CapsuleCollider charCollider;
    private bool space;
    private bool ledgeGrabbed;

    private float moveFoward;
    private float moveSideways;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        charCollider = this.transform.GetComponent<CapsuleCollider>();
    }
    private IEnumerator LedgeGrab()
    {
        Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit bodyCheck, 1f);
        Physics.Raycast(this.transform.position + new Vector3(0, 1, 0), this.transform.TransformDirection(Vector3.forward), out RaycastHit headCheck, 1f);
       if (bodyCheck.collider != null && headCheck.collider == null){
            Debug.Log("Ledge Grabed");
            rigidB.velocity = Vector3.zero;
            ledgeGrabbed = true;
            rigidB.AddForce(transform.up * (12f), ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
            rigidB.AddForce(transform.forward * (25), ForceMode.Impulse);
            ledgeGrabbed = false;
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
        return Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck25, 1.2f); ;
    }

    void Jump()
    {
        if (!ledgeGrabbed && CheckGround() && jumpCD == 0)
        {
            jumping = true;
            Debug.Log("Jumping");
            rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpCD = 0.2f;
        }
    }

    void Update()
    {
        moveFoward = 0;
        moveSideways = 0;

        //Debug.Log(rigidB.velocity);

        isOnGround = CheckGround();

        if (isOnGround)
        {
            ledgeGrabbed = false;
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

        if (Input.GetKey(KeyCode.S))
        {
            moveFoward = -movementSpeed;
        }

        if (!sliding)
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveSideways = -movementSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveSideways = movementSpeed;
            }
        }            

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isOnGround && !ledgeGrabbed)
            {
                IEnumerator coroutine = LedgeGrab();
                StartCoroutine(coroutine);
                LedgeGrab();
            }
            else
            {

                Jump();
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && isOnGround)
        {
            sliding = true;

        }
        else if(sliding)
        {
            Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.up), out RaycastHit upCheck, 1.5f);
            if (upCheck.collider == null)
            {
                rigidB.AddForce(transform.forward * (30f), ForceMode.Impulse);
                sliding = false;
                this.transform.localScale = Vector3.one;
            } 
        }

            if (sliding) {
                moveFoward = movementSpeed * 0.75f;
                this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            if (!ledgeGrabbed)
            {
                rigidB.AddForce((-transform.up * 6.5f), ForceMode.Force);
                rigidB.velocity = (this.transform.forward * moveFoward) + (this.transform.right * moveSideways) + (this.transform.up * rigidB.velocity.y);
            }
           
            Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.blue);

            if (jumpCD > 0)
            {
                jumpCD -= Time.deltaTime;
            }
            else
            {
                jumpCD = 0;
            }
    }

        
}
