using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float healthPoints = 3;
    private float hitFlashcooldown = 0f;
    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (healthPoints == 0)
        {
            var killEnemy = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
            killEnemy.removeEnemy(gameObject);
            
            MaskMeter.meter = Mathf.Clamp(MaskMeter.meter+30,0,300);
            
            Destroy(gameObject);   
        }


        if (hitFlashcooldown > 0)
        {
            hitFlashcooldown -= Time.deltaTime;
        } else {
            hitFlashcooldown = 0;
            if (mesh != null) {
                mesh.material.color = Color.yellow;
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            hitFlashcooldown = 0.4f;
            if (mesh != null)
            {
                mesh.material.color = Color.red;
            }
            healthPoints = healthPoints - 1;
        }
    }
}
