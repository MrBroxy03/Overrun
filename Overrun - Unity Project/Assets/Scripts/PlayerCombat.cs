using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float punchTimer = 0f;
    public bool canPunch = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out RaycastHit bodyCheck, 15f);
            Debug.DrawRay (this.transform.position, this.transform.TransformDirection (Vector3.forward), Color.blue);
        }
    }
}
