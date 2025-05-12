using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{

    public Transform player_Transform;
    public GameObject bullet;
   
    private List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        foreach (GameObject badGuys in  GameObject.FindGameObjectsWithTag("Enemy")){
            enemies.Add(badGuys);
        };
    }

    public void removeEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public GameObject getEnemy(GameObject player)
    {
        float nearestEnemy = 9999;
        GameObject targetEnemy = null;
        foreach(GameObject agusto in enemies){
            if (agusto != null)
            {
                float distance = Vector3.Distance(agusto.transform.position, player.transform.position);
                if (distance < 50 && distance < nearestEnemy)
                {
                    nearestEnemy = distance;
                    targetEnemy = agusto;
                }
            }
        };

        if (targetEnemy != null)
        {
            return targetEnemy;
        }

        return null;
    }

}
