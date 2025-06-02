using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public EnemyBehaviour enemy;

    private EnemyManager enemyManager;
    public float spotDistance = 10f;
    public float shootCooldown = 0f;
    public float coneOfVisionRadius = 45f;
    public float attackRange = 10f;

    public Animator animEnemy;
    public Animator animGun;

    private void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        animGun.Play("isIdle", 0, 0f);
        enemy = new EnemyBehaviour(this.gameObject, enemyManager.player_Transform, coneOfVisionRadius, spotDistance, attackRange);
        GetAnim();
    }


    private void Update()
    {
        if (enemy != null)
        {
            State CanSee = enemy.CanSeePlayer();
            if (CanSee == State.GoToPlayer)
            {
                //animEnemy.SetBool("isIdle", false);
                enemy.LookAtPlayer();
                
                if (shootCooldown == 0f)
                {
                    animEnemy.SetBool("isIdle", false);
                    animEnemy.SetBool("isSpot", false);
                    animGun.SetBool("isIdle", false);
                    animGun.SetBool("isSpot", false);
                    ShootBullet();
                } else 
                {
                    animEnemy.SetBool("isSpot", true);
                    animEnemy.SetBool("isShooting", false);
                    animGun.SetBool("isSpot", true);
                    animGun.SetBool("isShooting", false);
                }
                   
            } else 
            {
                animEnemy.SetBool("isIdle", true);
                animGun.SetBool("isIdle", true);
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
            animEnemy.SetBool("isShooting", true);
            animGun.SetBool("isShooting", true);
            shootCooldown = 2;
            Instantiate(enemyManager.bullet, this.transform.position, this.transform.rotation);
        }
    }

    void GetAnim()
    {
        if (animEnemy == null)
        {
            animEnemy = GetComponent<Animator>();
        }

        if (animGun == null)
        {
            animGun = GetComponent<Animator>();
        }
    }


}