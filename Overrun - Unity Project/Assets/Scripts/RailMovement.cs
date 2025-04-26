using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RailMovement : MonoBehaviour
{
    [SerializeField]
    private SplineAnimate splineAnimate;
    private SplineContainer spline;
    private bool startMovement = false;
    private float movementTimeout = 0f;
    private float railDuration = 0f;
    private float railTimer = 0f;
    private Rigidbody rb;
    bool jumping = MovementController.jumping;
    int meter = MaskMeter.meter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        splineAnimate = GetComponent<SplineAnimate>();
        railDuration = splineAnimate.Duration;
        if (splineAnimate != null)
        {
            splineAnimate.enabled = false;
        }
    }

    void Update()
    {
        Debug.Log(railDuration);
        if (railTimer >= railDuration  && startMovement == true)
        {
            movementTimeout = 0.5f;
            transform.eulerAngles = new Vector3(0, gameObject.transform.rotation.y*180, 0);
            rb.AddForce((transform.up * 10), ForceMode.Impulse);
            rb.AddForce((transform.forward * 30), ForceMode.Impulse);
            splineAnimate.enabled = false;
            startMovement = false;
            railTimer = 0;
            MovementController.jumping = true;
        }

        if (movementTimeout > 0)
        {
            movementTimeout -= Time.deltaTime;
        } else {
            movementTimeout = 0;
        }
        if (startMovement)
        {
            railTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rail") && movementTimeout == 0)
        {
            if (splineAnimate != null & startMovement == false)
            {
                splineAnimate.enabled = true;
                splineAnimate.Restart(true);
                startMovement = true;
                MaskMeter.meter += 100;
            }
        }
    }

}