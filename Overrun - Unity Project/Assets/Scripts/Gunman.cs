using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public Enemy enemy;

    public float spotDistance = 10f;
    public float shootCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 10f;

    private void Start()
    {
       enemy = new Enemy(this.gameObject, player, coneOfVisionRadius, spotDistance, attackRange);  
    }


    private void Update()
    {
        if (enemy != null)
        {
            State CanSee = enemy.CanSeePlayer();
            if (CanSee == State.GoToPlayer)
            {
                enemy.LookAtPlayer();
                ShootBullet();
            }
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
            shootCooldown = 2;
            Instantiate(bullet, this.transform.position, this.transform.rotation);
        }
    }


}