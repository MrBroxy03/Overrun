using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 8.0f;
    public float jumpForce = 3f;
    private float jumpCD = 0f;
    private float gravity = 750f;
    public bool sliding = false;
    public static bool jumping = false;
    public bool isOnGround = true;
    public bool isBoosting = false;
    private bool camZoomOut;


    public float LedgeVerticalPower = 3f;

    public Rigidbody rigidB;
    public Camera cam;

    public GameObject rail;

    private bool space;
    public bool ledgeGrabbed;

    private float moveFoward;
    private float moveSideways;

    int meter = MaskMeter.meter;

    // Start is called before the first frame update
    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        cam.fieldOfView = 60.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        isOnGround = CheckGround();
        if (isOnGround)
        {
            ledgeGrabbed = false;
            jumping = false;
        }
        moveFoward = 0;
        moveSideways = 0;

        if (!isBoosting && !ledgeGrabbed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit fowardCheck, 1f);
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
                    isOnGround = false;
                    LedgeGrab();
                }
                else
                {
                    Jump();
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

        if (Input.GetKey(KeyCode.LeftControl) && isOnGround)
        {
            sliding = true;

        }
        else if (sliding)
        {
            Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.up), out RaycastHit upCheck, 1.5f);
            if (upCheck.collider == null)
            {
                rigidB.AddForce(transform.forward * (30f), ForceMode.Impulse);
                sliding = false;
                this.transform.localScale = Vector3.one;
            }
        }

        if (Input.GetKey(KeyCode.Q) && MaskMeter.meter > 0)
        {
            movementSpeed = 40f;
            moveFoward = movementSpeed;

            isBoosting = true;
            cameraEffect();
            MaskMeter.meter -= 1;
        }
        else
        {
            movementSpeed = 8.0f;
            isBoosting = false;
        }
        if (jumpCD > 0)
        {
            jumpCD -= Time.deltaTime;
        }
        else
        {
            jumpCD = 0;
        }

        if (sliding)
        {
            moveFoward = movementSpeed * 0.75f;
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (!ledgeGrabbed)
        {
            rigidB.AddForce((-transform.up * gravity) * Time.deltaTime, ForceMode.Force);
            rigidB.velocity = ((this.transform.forward * moveFoward) + (this.transform.right * moveSideways) + (this.transform.up * rigidB.velocity.y));
        }

        if (cam.fieldOfView > 60 && !isBoosting)
        {
            cam.fieldOfView -= 10f;
            camZoomOut = false;
        }

        if (cam.fieldOfView < 60) 
        {
            cam.fieldOfView = 60f;
        }
    }

    private IEnumerator DoLedgeGrab()
    {
        ledgeGrabbed = true;
        rigidB.velocity = Vector3.zero;
        rigidB.AddForce(transform.up * (8f), ForceMode.Impulse);
        float positionY = this.transform.position.y + 2f;
        while (this.transform.position.y < positionY)
        {
            yield return new WaitForSeconds(0.05f);
        }
        rigidB.AddForce(transform.forward * (8f), ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        ledgeGrabbed = false;
    }
    private void LedgeGrab()
    {
        Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit bodyCheck, 1f);
        Physics.Raycast(this.transform.position + new Vector3(0, 1, 0), this.transform.TransformDirection(Vector3.forward), out RaycastHit headCheck, 1f);
        if (bodyCheck.collider != null && headCheck.collider == null)
        {
            IEnumerator coroutine = DoLedgeGrab();
            StartCoroutine(coroutine);
        }
    }

    public void JumpOnEnemy()
    {
        rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private bool CheckGround()
    {
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1f, Color.red);

        Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck2, 1f);
        if (bodyCheck2.collider != null && bodyCheck2.collider.gameObject.CompareTag("Enemy")){
            return false;
        }
        return Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck25, 1f); ;
    }

    void Jump()
    {
        if (!ledgeGrabbed && jumpCD == 0)
        {
            jumping = true;
            
            rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpCD = 0.2f;
        }
    }

    public void ChangeGravity(float changedGravity)
    {
       gravity = changedGravity;
    }

    void cameraEffect()
    {
        if (isBoosting && !camZoomOut)
        {
            if (cam.fieldOfView < 90)
            {
                cam.fieldOfView += 1.2f;
            }
        }

    }


}