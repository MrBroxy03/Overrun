using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float walkSpeed = 8.0f;
    public float crouchwalkSpeed = 3.0f;
    public float boostSpeed = 30f;

    private float desiredSpeedZ = 8.0f;
    private float desiredSpeedX = 8.0f;
    public float speedX,speedZ = 0;

    private float acceleration = 0.2f;

    public float walkacceleration = 0.2f;
    public float boostacceleration = 0.75f;

    public float slidedeacceleration = 3f;
    public float crouchacceleration = 0.4f;

    public float jumpForce = 3f;
    private float jumpCD = 0f;
    private float gravity = 750f;

    public bool sliding = false;
    public static bool jumping = false;
    public bool isOnGround = true;
    public bool isBoosting = false;

    private int directionZ,directionX;

    public Rigidbody rigidB;

    public GameObject rail;

    public bool ledgeGrabbed;

    // Start is called before the first frame update
    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }
    private float AccelerrationFunc(float speed,float desiredSpeed,float dt)
    {
        Debug.Log(desiredSpeed);
        Debug.Log(speed);
        if (speed != desiredSpeed) {
            if (Mathf.Abs(speed - desiredSpeed) < 1f)
            {
                speed = desiredSpeed;

            }

            if (speed < desiredSpeed)
            {
                speed += (20f / acceleration) * dt;
            }

            if (speed > desiredSpeed)
            {
                speed -= (15f / acceleration) * dt;
            }
        } 
        return speed;
    }
    private void SpeedFunc()
    {
        float dt = Time.deltaTime;

        desiredSpeedZ = walkSpeed;
        desiredSpeedX = walkSpeed;

        acceleration = walkacceleration;

        if (directionX == 0 && directionZ == 0)
        {
            desiredSpeedX = 0;
            desiredSpeedZ = 0;
        }

        if (jumpCD > 0)
        {
            jumpCD = Mathf.Clamp(jumpCD - dt, 0, jumpCD);
        }

        if (Input.GetKey(KeyCode.Q) && MaskMeter.meter > 0 && !sliding)
        {
            desiredSpeedZ = boostSpeed;
            acceleration = boostacceleration; 
            isBoosting = true;
            MaskMeter.meter -= 1;
        }
        else if (sliding)
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (speedX > crouchwalkSpeed || speedZ > crouchwalkSpeed)
            {
                desiredSpeedZ = 0;
                desiredSpeedX = 0;
                acceleration = slidedeacceleration;
            }
            else {
                desiredSpeedZ = crouchwalkSpeed;
                desiredSpeedX = crouchwalkSpeed;
                acceleration = crouchacceleration; 
            }
           
           
            isBoosting = false;
        }

        desiredSpeedZ *= directionZ;
        desiredSpeedX *= directionX;

        speedZ = AccelerrationFunc(speedZ,desiredSpeedZ,dt);
        speedX = AccelerrationFunc(speedX,desiredSpeedX,dt);


        if (!ledgeGrabbed)
        {
            rigidB.AddForce((-transform.up * gravity) * dt, ForceMode.Force);

            rigidB.velocity = (this.transform.forward * speedZ) + (this.transform.right * speedX) + (this.transform.up * rigidB.velocity.y);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (!MenuSystem.paused)
        {
            isOnGround = CheckGround();
            if (isOnGround)
            {
                ledgeGrabbed = false;
                jumping = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                directionZ = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                directionZ = -1;
            }
            else
            {
                directionZ = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                directionX = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                directionX = -1;
            }
            else
            {
                directionX = 0;
            }


            if (Input.GetKey(KeyCode.Space))
            {
                sliding = false;
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
                    sliding = false;
                    this.transform.localScale = Vector3.one;
                }
            }

            SpeedFunc();
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