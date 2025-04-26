using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public Enemy enemy;

    public float spotDistance = 10f;
    private float spottedPlayerCooldown = 0f;
    private float shootCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 7f;

    State enemyState = State.Idle;
    private void Start()
    {
       enemy = new Enemy(this.gameObject, player, coneOfVisionRadius, spotDistance,attackRange);  
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
                if (enemyState == State.AttackPlayer)
                {
                    ShootBullet();
                }
                else
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
            spottedPlayerCooldown -= Time.deltaTime;
        }
        else
        {
            spottedPlayerCooldown = 0f;
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            shootCooldown = 0f;
        }
    }

    void ShootBullet()
    {
        if (shootCooldown == 0f)
        {
            shootCooldown = 1.5f;
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
    }


}