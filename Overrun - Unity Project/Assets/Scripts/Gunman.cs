using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;

    public float rayDistance = 20f;
    public float shootCooldown = 0f;

    void Update()
    {
        
        if (Physics.Raycast(player.transform.position, -transform.TransformDirection (Vector3.forward), out RaycastHit hitinfo, rayDistance) && shootCooldown == 0f)
        {
            ShootBullet();
            shootCooldown = 0.5f;
            //Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);
        } else {
            //Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * rayDistance, Color.blue);
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        } else {
            shootCooldown = 0f;
        }
    }

    void ShootBullet()
    {
        Instantiate(bullet, this.transform.position, this.transform.rotation);
        
    }

}
