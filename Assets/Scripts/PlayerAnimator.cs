using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator playerAnimatorComponent;
    CharController playerController;

    bool hasSwung;
    Coroutine swingCoroutine;

    void Awake()
    {
        playerAnimatorComponent = GetComponentInChildren<Animator>();
        playerController = GetComponent<CharController>();
    }

    void Start()
    {
        playerAnimatorComponent.SetInteger("State", 1);
    }

    void Update()
    {
        //current player state (atk, def, utl)
        StanceState stance = playerController.currentStanceState;

        //check if left controller stick is moved
        if (playerController.controllerInputLeftStick != Vector2.zero)
        {
            playerAnimatorComponent.SetInteger("State", 0);
        }
        else
        {
            playerAnimatorComponent.SetInteger("State", (int)stance + 1);
        }
        
        //cool guy version
        //anim.SetInteger("State", boob.controllerInputLeftStick != Vector2.zero ? 0 : (int)boob.currentStanceState + 1);
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
        playerAnimatorComponent.SetTrigger("AttackSword1");

        float clipLength = -1;
        float clip2Length = -1;
        foreach (AnimationClip c in playerAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (c.name == "AttackSword1")
            {
                clipLength = c.length;
            }
            if (c.name == "AttackSword2")
            {
                clip2Length = c.length;
            }
        }

        //prevent a second attack
        float timer = clipLength * 0.75f;//first 75% of the animation, dont allow the second attack
        while (timer > 0)
        {
            hasSwung = false;
            playerAnimatorComponent.SetBool("AttackSword2", hasSwung);

            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //small window for second attack check
        bool swinging = false;
        float hitWindow = clipLength * 0.25f;//25% of the end of the animation
        while (hitWindow > 0)
        {
            //if we've pressed the attack button during the window swing into the second attack
            if (hasSwung)
            {
                //wait for the second animation to end
                swinging = true;
                print("swing");
                playerAnimatorComponent.SetBool("AttackSword2", hasSwung);
            }

            hitWindow -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //wait longer for the second animation if true
        if (swinging)
        {
            yield return new WaitForSeconds(clip2Length);
        }

        hasSwung = false;
        if (!swinging)
        {
            playerAnimatorComponent.SetBool("AttackSword2", hasSwung);
        }

        swingCoroutine = null;
        playerController.isAttacking = false;
    }
}
