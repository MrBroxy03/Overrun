using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float walkSpeed = 8.0f;
    public float boostSpeed = 30f;
    private float maxSpeed = 8.0f;
    private float speedX,speedZ;
    public float acceleration = 0.2f;
    public float jumpForce = 3f;
    private float jumpCD = 0f;
    private float gravity = 750f;
    public bool sliding = false;
    public static bool jumping = false;
    public bool isOnGround = true;
    public bool isBoosting = false;


    public float LedgeVerticalPower = 3f;

    public Rigidbody rigidB;

    public GameObject rail;

    private bool space;
    public bool ledgeGrabbed;

    // Start is called before the first frame update
    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {

        float dt = Time.deltaTime;

        isOnGround = CheckGround();
        if (isOnGround)
        {
            ledgeGrabbed = false;
            jumping = false;
        }

        if (!isBoosting && !ledgeGrabbed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                speedZ += (maxSpeed/0.2f)*dt;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                speedZ += -(maxSpeed / 0.2f) * dt;
            }
            else
            {
                speedZ = 0f;
            }

            if (!sliding)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    speedX += -(maxSpeed / 0.2f) * dt;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    speedX += (maxSpeed / 0.2f) * dt;
                }
                else
                {
                    speedX = 0f;
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
            if (!jumping && isOnGround)
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
            maxSpeed = boostSpeed;
            isBoosting = true;
            MaskMeter.meter -= 1;
        }
        else
        {
            maxSpeed = walkSpeed;
            isBoosting = false;
        }
        
        if (jumpCD > 0)
        {
            jumpCD = Mathf.Clamp(jumpCD - dt, 0, jumpCD);
        }
       

        if (sliding)
        {
            speedZ = maxSpeed * 0.75f;
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (!ledgeGrabbed)
        {
            rigidB.AddForce((-transform.up * gravity) * Time.deltaTime, ForceMode.Force);

            speedZ = Mathf.Clamp(speedZ, -maxSpeed, maxSpeed);
            speedX = Mathf.Clamp(speedX,-maxSpeed, maxSpeed);

            rigidB.velocity = ((this.transform.forward * speedZ) + (this.transform.right * speedX) + (this.transform.up * rigidB.velocity.y));
        }
    }

    private IEnumerator DoLedgeGrab()
    {
        ledgeGrabbed = true;
        rigidB.velocity = Vector3.zero;
        rigidB.AddForce(transform.up * (6f), ForceMode.Impulse);
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
        Debug.DrawRay(this.transform.position, -this.transform.TransformDirection(Vector3.up) * 1.05f, Color.red);
        Physics.Raycast(this.transform.position, -this.transform.TransformDirection(Vector3.up), out RaycastHit bodyCheck2, 1.05f);
        if (bodyCheck2.collider != null && !bodyCheck2.collider.gameObject.CompareTag("Enemy")) {
            return true;
        }
        return false;
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




}