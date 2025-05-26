using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesAnimations : MonoBehaviour
{
    public MovementController playerMovement;
    public PlayerCombat playerCombat;

    public GameObject arms;
    public Animator anim;

    private bool hasLeftGround = false;

    void Start()
    {
        GetAnim();
        playerMovement = GetComponent<MovementController>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        bool isOnGround = playerMovement.isOnGround;
        bool isPunching = playerCombat.isPunching;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Handle jumping / falling logic
        if (!isOnGround)
        {
            // Player just started jumping
            if (!hasLeftGround)
            {
                hasLeftGround = true;
                arms.SetActive(true);
                anim.SetBool("isJumping", true);
                anim.SetBool("isFalling", false);
            }

            // Player is mid-air and jump animation played enough
            if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 0.4f)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
                Debug.Log("I'm falling");
            }
        }
        else
        {
            // Player landed
            if (hasLeftGround)
            {
                hasLeftGround = false;
                arms.SetActive(false);
            }

            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        // Handle punching logic
        if (isPunching)
        {
            arms.SetActive(true);
            anim.SetBool("isPunching", true);
        }
        else
        {
            anim.SetBool("isPunching", false);
        }
    }

    void GetAnim()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }
}
