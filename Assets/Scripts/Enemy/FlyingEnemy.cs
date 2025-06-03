using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public EnemyBehaviour enemy;
    private EnemyManager enemyManager;
    public float spotDistance = 10f;
    private float spottedPlayerCooldown = 0f;
    private float shootCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 7f;

    State enemyState = State.Idle;
    private void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        enemy = new EnemyBehaviour(this.gameObject,enemyManager.player_Transform, coneOfVisionRadius, spotDistance,attackRange);  
    }


    private void Update()
    {
        if (enemy != null)
        {
            
            enemyState = enemy.CanSeePlayer();
            if (enemyState == State.GoToPlayer)
            {
                spottedPlayerCooldown = 2f;
                enemyState = enemy.InRange();
                enemy.LookAtPlayer();
                if (enemyState == State.AttackPlayer && shootCooldown == 0)
                {
                    ShootBullet();
                }
                else if(enemyState != State.AttackPlayer)
                {
                    enemy.GotoPlayer();
                }
            }
            else if(spottedPlayerCooldown == 0)
            {

                enemy.BackToSpot();
            }
            else
            {
                enemy.Stop();
            }
        }

        if (spottedPlayerCooldown > 0)
        {
            spottedPlayerCooldown = Mathf.Clamp(spottedPlayerCooldown - Time.deltaTime, 0, spottedPlayerCooldown);
        }

        if (shootCooldown > 0)
        {
            shootCooldown = Mathf.Clamp(shootCooldown - Time.deltaTime, 0, shootCooldown);
        }
    }
    void ShootBullet()
    {
        if (shootCooldown == 0f)
        {
            Vector3 direction = this.transform.position - enemy.player.position;
            shootCooldown = 2;
            Instantiate(enemyManager.bullet, this.transform.position, Quaternion.FromToRotation(-Vector3.forward, direction));
        }
    }
}