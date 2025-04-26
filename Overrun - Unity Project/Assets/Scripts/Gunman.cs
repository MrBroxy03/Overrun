using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public Enemy enemy;

    private EnemyManager enemyManager;
    public float spotDistance = 10f;
    public float shootCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 10f;

    private void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();

        enemy = new Enemy(this.gameObject, enemyManager.player_Transform, coneOfVisionRadius, spotDistance, attackRange);

    }


    private void Update()
    {
        if (enemy != null)
        {
            State CanSee = enemy.CanSeePlayer();
            if (CanSee == State.GoToPlayer)
            {
                enemy.LookAtPlayer();
                if (shootCooldown == 0f)
                {
                    ShootBullet();
                }
                   
            }
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
            shootCooldown = 2;
            Instantiate(enemyManager.bullet, this.transform.position, this.transform.rotation);
        }
    }


}