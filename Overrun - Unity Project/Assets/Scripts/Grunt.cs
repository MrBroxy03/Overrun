using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    public Transform player;
    public Enemy enemy;

    public List<GameObject> waypoints = new List<GameObject>();
    float speed = 10f;
    float rotSpeed = 10f;
    int accuracy = 3;
    GameObject currentWP;
    int currentWPIndex = 0;

    public float spotDistance = 10f;
    private float spottedPlayerCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 0f;

    State enemyState = State.Idle;

    private void Start()
    {
       enemy = new Enemy(this.gameObject, player, coneOfVisionRadius, spotDistance,attackRange);  

       currentWP = waypoints[0];
    }

    private void Update()
    {
        if (enemy != null)
        {
            enemyState = enemy.CanSeePlayer();
            if (enemyState == State.GoToPlayer)
            {
                enemyState = enemy.InRange();
                enemy.LookAtPlayer();
                if (enemyState == State.AttackPlayer)
                {
                    enemy.GotoPlayer();
                }
                else
                {
                    enemy.GotoPlayer();
                }
            } else {
                if (Vector3.Distance(this.transform.position, currentWP.transform.position) < accuracy)
                {
                    currentWPIndex++;
                    currentWP = waypoints[currentWPIndex];
                }

                if (currentWPIndex >= waypoints.Count - 1)
                {
                    currentWPIndex = 0;
                }

                Quaternion lookat = Quaternion.LookRotation(currentWP.transform.position - this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookat, rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }

        if (spottedPlayerCooldown > 0)
        {
            spottedPlayerCooldown -= Time.deltaTime;
        }
        else
        {
            spottedPlayerCooldown = 0f;
        }
    }
}
