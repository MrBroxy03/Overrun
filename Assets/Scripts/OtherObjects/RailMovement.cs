using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RailMovement : MonoBehaviour
{
    [SerializeField]
    public SplineAnimate splineAnimate;
    public PlayerCombat playerCombat;
    public HomingAttack homingAttack;
    public SplineContainer spline;
    public bool startMovement = false;
    private float movementTimeout = 0f;
    private float railDuration = 0f;
    private float railTimer = 0f;
    private Rigidbody rb;
    bool jumping = MovementController.jumping;

    public AudioClip railingSound;
    private AudioSource railingsource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        splineAnimate = GetComponent<SplineAnimate>();
        playerCombat = GetComponent<PlayerCombat>();
        homingAttack = GetComponent<HomingAttack>();

        railDuration = splineAnimate.Duration;
        if (splineAnimate != null)
        {
            splineAnimate.enabled = false;
        }
    }

    void Update()
    {
        if (railTimer >= railDuration  && startMovement == true)
        {
            movementTimeout = 0.5f;
            transform.eulerAngles = new Vector3(0, gameObject.transform.rotation.y*180, 0);
            rb.AddForce((transform.up * 10), ForceMode.Impulse);
            rb.AddForce((transform.forward * 30), ForceMode.Impulse);

            Destroy(railingsource);

            splineAnimate.enabled = false;
            playerCombat.enabled = true;
            homingAttack.enabled = true;
            PlayerCombat.isGroundPound = false;

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
            SplineContainer container = collision.gameObject.GetComponentInParent<SplineContainer>();
            if (container != null)
            {
                this.splineAnimate.Container = container;
            }
            
            if (splineAnimate != null & startMovement == false)
            {
                splineAnimate.enabled = true;
                playerCombat.enabled = false;
                homingAttack.enabled = false;
                PlayerCombat.isGroundPound = false;
                splineAnimate.Restart(true);
                startMovement = true;

                railingsource = SoundEffects.instance.PlayLoopSFXClip(railingSound, this.transform);
                MaskMeter.meter = Mathf.Clamp(MaskMeter.meter+150,0, MaskMeter.maxMeter);
            }
        }
    }

}