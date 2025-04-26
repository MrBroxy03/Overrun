using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float shootSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, shootSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") )
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
