using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private MovementController MvController;

    public bool isPunching = false;
    public bool canPunch = false;

    public float punchTimer = 0f;
    public float punchAttack = 0f;


    public float groundPoundAttack;
    public bool isGroundPound = false;

    private GameObject punchHitbox;
    public GameObject punchHitboxPrefab;
    private GameObject gpHitbox;
    public GameObject gpHitboxPrefab;

    public CameraShake cameraShake;


    void Start()
    {
        MvController = GetComponent<MovementController>();
    }

    private void groundPound()
    {
        if (isGroundPound)
        {
            MvController.ChangeGravity(7000f);
        }
        else
        {
            MvController.ChangeGravity(750f);
        }
    }

    private void Update()
    {
        if (!MenuSystem.paused)
        {
            bool isOnGround = MvController.isOnGround;

            if (isOnGround && isGroundPound)
            {
                Destroy(gpHitbox);
                gpHitbox = Instantiate(gpHitboxPrefab, this.transform.position, this.transform.rotation);
                groundPoundAttack = 0.5f;
                isGroundPound = false;
                groundPound();
                StartCoroutine(cameraShake.Shaking(.20f, .1f));
            }

            if (Input.GetKey(KeyCode.Mouse0) && !isPunching && punchAttack == 0)
            {
                isPunching = true;
                punchAttack = 0.5f;
                punchHitbox = Instantiate(punchHitboxPrefab, this.transform.position, this.transform.rotation);
            }

            if (punchAttack > 0)
            {
                punchAttack -= Time.deltaTime;
            }
            else if (punchAttack < 0)
            {
                isPunching = false;
                Destroy(punchHitbox);
                punchAttack = 0;
            }

            if (Input.GetKey(KeyCode.Mouse1) && !isOnGround && !isGroundPound)
            {
                isGroundPound = true;
                groundPound();
                Destroy(gpHitbox);
            }

            if (groundPoundAttack > 0 && gpHitbox != null)
            {
                groundPoundAttack -= Time.deltaTime;
                gpHitbox.transform.position = this.transform.position;
            }
            else if (groundPoundAttack < 0 && gpHitbox != null)
            {
                Destroy(gpHitbox);
                groundPoundAttack = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && MvController.isBoosting)
        {
            Destroy(collision.gameObject);
            MaskMeter.meter += 30;
        }
        if (collision.gameObject.CompareTag("Enemy") && isGroundPound)
        {
            Debug.Log("Bouncing");
            Destroy(gpHitbox);
            gpHitbox = Instantiate(gpHitboxPrefab, this.transform.position, this.transform.rotation);
            gpHitbox.transform.localScale = gpHitboxPrefab.transform.localScale * 0.5f;
            MvController.JumpOnEnemy();
            isGroundPound = false;
            groundPound();
            groundPoundAttack = 1f;
            MaskMeter.meter += 30;
            StartCoroutine(cameraShake.Shaking(.20f, .1f));
        }

    }
}
