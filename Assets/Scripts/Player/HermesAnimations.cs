using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermesarmsations : MonoBehaviour
{
    public MovementController playerMovement;
    public PlayerCombat playerCombat;

    public GameObject arm;
    public Animator arms;

    private bool hasLeftGround = false;

    void Start()
    {
        GetArms();
        playerMovement = GetComponent<MovementController>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        bool isOnGround = playerMovement.isOnGround;
        bool isPunching = playerCombat.isPunching;

        AnimatorStateInfo stateInfo = arms.GetCurrentAnimatorStateInfo(0);

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

    void GetArms()
    {
        if (arms == null)
        {
            arms = GetComponent<Animator>();
        }
    }
}
