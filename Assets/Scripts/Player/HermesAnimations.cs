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
    private bool isDoingGP = false;
    private bool hasGroundPounded = false;


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
        bool ledgeGrab = playerMovement.ledgeGrabbed;
        bool isPunching = playerCombat.isPunching;
        bool isGP = PlayerCombat.isGroundPound;
        bool isGrinding = railMovement.startMovement;

        AnimatorStateInfo stateInfo = arms.GetCurrentAnimatorStateInfo(0);

        if (!isGrinding)
        {
            arms.SetBool("isStartRail", false);
            arms.SetBool("isRailIdle", false);

            if (ledgeGrab)
            {
                arms.SetBool("isLedgeGrabbing", true);
            }

            if (stateInfo.IsName("LedgeGrab") && stateInfo.normalizedTime > 0.7f) 
            {
                arms.SetBool("isLedgeGrabbing", false);
            }

            if (!isOnGround)
            {
                if (!hasLeftGround)
                {
                    hasLeftGround = true;

                    arm.SetActive(true);
                    arms.SetBool("isJumping", true);
                    arms.SetBool("isFalling", false);

                    isDoingGP = false;
                    hasGroundPounded = false;
                }

                if (stateInfo.IsName("Jump") && stateInfo.normalizedTime >= 0.4f && !ledgeGrab)
                {
                    arms.SetBool("isJumping", false);
                    arms.SetBool("isFalling", true);
                }

                if (isGP && !isDoingGP)
                {
                    isDoingGP = true;
                    arms.SetBool("startGP", true);
                }
            }
            else
            {
                if (isDoingGP && !hasGroundPounded)
                {
                    arms.SetBool("startGP", false);
                    arms.SetBool("attackGP", true);
                    hasGroundPounded = true;
                }

                if (hasLeftGround)
                {
                    hasLeftGround = false;
                }

                arms.SetBool("isJumping", false);
                arms.SetBool("isFalling", false);

                if (arms.GetCurrentAnimatorStateInfo(0).IsName("GroundPoundAttack") && stateInfo.normalizedTime >= 0.35f)
                {
                    arms.SetBool("attackGP", false);
                    isDoingGP = false;
                    hasGroundPounded = false;
                }
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

        bool noAnims = stateInfo.IsName("Idle") && !arms.GetBool("isJumping") &&
        !arms.GetBool("isFalling") && !arms.GetBool("startGP") &&
        !arms.GetBool("attackGP") && !arms.GetBool("isPunching") &&
        !arms.GetBool("isStartRail") && !arms.GetBool("isRailIdle");

        if (noAnims)
        {
            arm.SetActive(false);
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
