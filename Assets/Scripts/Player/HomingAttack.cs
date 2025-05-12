using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomingAttack : MonoBehaviour
{
    int meter = MaskMeter.meter;
    bool jumping = MovementController.jumping;
    public bool homingAttack = false;
    public float attackTimeout = 0f;
    private float raycast = 60f;
    private Rigidbody rigidB;

    public CameraShake cameraShake;
    public RectTransform crosshair;
    public Camera cam;

    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        crosshair.gameObject.SetActive(false);
    }

    void Update()
    {
        var killEnemy = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
        GameObject targetEnemy = killEnemy.getEnemy(gameObject);
        
        bool showCrosshair = false;
        if (MovementController.jumping)
        {
            if (targetEnemy.transform.position.y < this.gameObject.transform.position.y && targetEnemy != null && MaskMeter.meter != 0 && attackTimeout == 0)
            {
                Vector3 dir = (targetEnemy.transform.position - this.transform.position).normalized;
                Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hitInfo, raycast);

                if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Enemy"))
                {
                    showCrosshair = true;
                    Vector3 screenPos = cam.WorldToScreenPoint(targetEnemy.transform.position);
                    crosshair.position = screenPos;

                    if (Input.GetKey(KeyCode.Mouse0) && !homingAttack)
                    {
                        homingAttack = true;
                        MaskMeter.meter -= 70;
                    }
                }
                Debug.DrawRay(Camera.main.transform.position, dir * raycast, Color.blue);
            }
        }

        crosshair.gameObject.SetActive(showCrosshair);

        if (homingAttack)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, 20 * Time.deltaTime);
        }

        if (attackTimeout > 0)
        {
            attackTimeout -= Time.deltaTime;
        } 

        if (attackTimeout < 0)
        {
            attackTimeout = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && homingAttack)
        {
            homingAttack = false;
            Debug.Log("Doing for you");
            StartCoroutine(cameraShake.Shaking(.20f, .7f));
            Destroy(collision.gameObject);
            rigidB.AddForce(transform.up * 10, ForceMode.Impulse);
            MaskMeter.meter += 5;
            attackTimeout = 1f;
            MovementController.jumping = true;
        }
    }

    
}
