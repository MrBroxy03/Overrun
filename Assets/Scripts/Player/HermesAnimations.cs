using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesAnimations : MonoBehaviour
{
    public MovementController playerMovement;
    public RailMovement railMovement;
    public PlayerCombat playerCombat;

    public GameObject arm;
    public Animator arms;

    private bool hasLeftGround = false;

    void Start()
    {
        GetArms();
        playerMovement = GetComponent<MovementController>();
        railMovement = GetComponent<RailMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        bool isOnGround = playerMovement.isOnGround;
        bool isPunching = playerCombat.isPunching;
        bool isGrinding = railMovement.startMovement;

        AnimatorStateInfo stateInfo = arms.GetCurrentAnimatorStateInfo(0);

        if (!isGrinding)
        {
            arms.SetBool("isStartRail", false);
            arms.SetBool("isRailIdle", false);
            if (!isOnGround)
            {
                if (!hasLeftGround)
                {
                    hasLeftGround = true;

                    arm.SetActive(true);
                    arms.SetBool("isJumping", true);
                    arms.SetBool("isFalling", false);
                }

                if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 0.4f)
                {
                    arms.SetBool("isJumping", false);
                    arms.SetBool("isFalling", true);
                    Debug.Log("I'm falling");
                }
            }
            else
            {
                if (hasLeftGround)
                {
                    hasLeftGround = false;

                    arm.SetActive(false);
                }

                arms.SetBool("isJumping", false);
                arms.SetBool("isFalling", false);
            }

            if (isPunching)
            {
                arm.SetActive(true);
                arms.SetBool("isPunching", true);
            }
            else
            {
                arms.SetBool("isPunching", false);
            }
        } 
        else 
        {
            arms.SetBool("isJumping", false);
            arms.SetBool("isFalling", false);
            arms.SetBool("isPunching", false);

            if (stateInfo.normalizedTime < 0.4f) {
                arm.SetActive(true);
                arms.SetBool("isStartRail", true);
            } 
            else 
            {
                arms.SetBool("isStartRail", false);
                arms.SetBool("isRailIdle", true);
            }
        }
    }

    void GetArms()
    {
        if (arms == null)
        {
            arms = GetComponent<Animator>();
        }
    }
}
