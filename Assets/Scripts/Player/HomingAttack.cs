using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class HomingAttack : MonoBehaviour
{
    public int meter = MaskMeter.meter;
    public bool jumping = MovementController.jumping;
    public bool homingAttack = false;
    public float homingAttackRange = 50f;
    private float attackTimeout = 0f;
    private Rigidbody rigidB;

    public CameraShake cameraShake;
    public RectTransform crosshair;
    public Camera cam;

    private MovementController MvController;

    void Start()
    {
        MvController = GetComponent<MovementController>();

        rigidB = GetComponent<Rigidbody>();
        crosshair.gameObject.SetActive(false);
    }

    IEnumerator Invicible(bool boolean,float seconds)
    {
        if (seconds > 0) { 
            yield return new WaitForSeconds(seconds);
        }
        homingAttack = boolean;
        Debug.Log(homingAttack);
    }

    void Update()
    {
        var killEnemy = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
        GameObject targetEnemy = killEnemy.GetEnemy(gameObject);
        
        bool showCrosshair = false;
        if (!MvController.isOnGround && targetEnemy != null)
        {
            Vector3 enemyPosition = targetEnemy.transform.position;
            Vector3 PlayerPosition = cam.transform.position;
            Vector3 dir = (enemyPosition - PlayerPosition).normalized;

            float cosAngle = Vector3.Dot(dir, cam.transform.forward);
            float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

          
            if (angle <= 15 && enemyPosition.y+.5 < PlayerPosition.y && (enemyPosition - PlayerPosition).magnitude < homingAttackRange && MaskMeter.meter != 0 && attackTimeout == 0 )
            {
                int distance = Convert.ToInt32(Math.Round((enemyPosition - PlayerPosition).magnitude)-1);
                Physics.Raycast(PlayerPosition, dir, out RaycastHit hitInfo, distance);
                if(hitInfo.collider == null)
                {
                    showCrosshair = true;
                    Vector3 screenPos = cam.WorldToScreenPoint(enemyPosition);
                    crosshair.position = screenPos;

                    if (Input.GetKey(KeyCode.Mouse0) && !homingAttack && MaskMeter.meter >= 70)
                    {
                        homingAttack = true;
                        MaskMeter.meter -= 70;
                    }
                }
                
            }
        }

        crosshair.gameObject.SetActive(showCrosshair);

        if (homingAttack && targetEnemy != null)
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
            Debug.Log("Doing for you");
            StartCoroutine(cameraShake.Shaking(.20f, .7f));
            Destroy(collision.gameObject);
            rigidB.AddForce(transform.up * 8, ForceMode.Impulse);
            MaskMeter.meter += 5;
            attackTimeout = 1f;
            MovementController.jumping = true;

            IEnumerator coroutine = Invicible(false, 0.3f);
            StartCoroutine(coroutine);
        }
    }

    
}
