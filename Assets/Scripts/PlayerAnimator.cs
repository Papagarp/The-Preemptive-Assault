using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    #region Variables

    Animator playerAnimatorComponent;
    CharController playerController;
    Coroutine swingCoroutine;

    bool jumpAnimPlaying;
    bool hasSwung;

    #endregion

    void Awake()
    {
        playerAnimatorComponent = GetComponentInChildren<Animator>();
        playerController = GetComponent<CharController>();
    }

    private void Update()
    {
        StanceState currentState = playerController.currentStanceState;

        playerAnimatorComponent.SetInteger("Current State", (int)currentState);

        if (playerController.hasJumped && !jumpAnimPlaying)
        {
            playerAnimatorComponent.SetTrigger("Jumping");

            float jumpAnimationLength = -1;

            foreach (AnimationClip clip in playerAnimatorComponent.runtimeAnimatorController.animationClips)
            {
                if(clip.name == "Main_char_jump_test_001")
                {
                    jumpAnimationLength = clip.length;
                }
                
            }

            print(jumpAnimationLength);

            while (jumpAnimationLength > 0)
            {
                jumpAnimPlaying = true;
                jumpAnimationLength -= Time.deltaTime;
            }

            if(playerController.isGrounded && jumpAnimationLength <= 0) jumpAnimPlaying = false;
        }

        if (playerController.controllerInputLeftStick != Vector2.zero)
        {
            playerAnimatorComponent.SetBool("Moving", true);
        }
        else
        {
            playerAnimatorComponent.SetBool("Moving", false);
        }
    }

    public void Swing()
    {
        playerController.isAttacking = true;
        hasSwung = true;

        //start the attack combo
        if (swingCoroutine == null)
        {
            swingCoroutine = StartCoroutine(AttackCombo());
        }
    }

    IEnumerator AttackCombo()
    {
        playerAnimatorComponent.SetTrigger("SwordAttack1");

        float firstAnimationLength = -1;
        float secondAnimationLength = -1;

        foreach (AnimationClip clip in playerAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "AttackSword1")
            {
                firstAnimationLength = clip.length;
            }
            if (clip.name == "AttackSword2")
            {
                secondAnimationLength = clip.length;
            }
        }

        //prevent a second attack
        float timer = firstAnimationLength * 0.75f;//first 75% of the animation, dont allow the second attack
        while (timer > 0)
        {
            hasSwung = false;
            playerAnimatorComponent.SetBool("SwordAttack2", hasSwung);

            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //small window for second attack check
        bool swinging = false;
        float hitWindow = firstAnimationLength * 0.25f;//25% of the end of the animation
        while (hitWindow > 0)
        {
            //if we've pressed the attack button during the window swing into the second attack
            if (hasSwung)
            {
                //wait for the second animation to end
                swinging = true;
                playerAnimatorComponent.SetBool("SwordAttack2", hasSwung);
            }

            hitWindow -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //wait longer for the second animation if true
        if (swinging)
        {
            yield return new WaitForSeconds(secondAnimationLength);
        }

        hasSwung = false;
        if (!swinging)
        {
            playerAnimatorComponent.SetBool("SwordAttack2", hasSwung);
        }

        swingCoroutine = null;
        playerController.isAttacking = false;
    }
}
