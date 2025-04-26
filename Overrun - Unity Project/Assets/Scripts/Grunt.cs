using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    public Enemy enemy;
    private EnemyManager enemyManager;
    public List<GameObject> waypoints = new List<GameObject>();
    public float speed = 5f;
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
       enemyManager = FindAnyObjectByType<EnemyManager>();
       enemy = new Enemy(this.gameObject,enemyManager.player_Transform, coneOfVisionRadius, spotDistance,attackRange);  

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
                enemy.GotoPlayer();
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

                enemy.GotoPlace(currentWP.transform);
            }
        }

        if (spottedPlayerCooldown > 0)
        {
            spottedPlayerCooldown = Mathf.Clamp(spottedPlayerCooldown - Time.deltaTime, 0, spottedPlayerCooldown);
        }
    }
}
