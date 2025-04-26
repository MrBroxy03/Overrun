using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float HP = 3;
    public float hitFlash = 0f;
    public MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (HP == 0)
        {
            var killEnemy = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
            killEnemy.removeEnemy(gameObject);
            Destroy(gameObject);   
        }

        if (hitFlash > 0)
        {
            hitFlash -= Time.deltaTime;
        } else {
            hitFlash = 0;
            mesh.material.color = Color.white;
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        { 
            hitFlash = 0.4f;
            mesh.material.color = Color.red;
            HP = HP - 1;
        }
    }
}
