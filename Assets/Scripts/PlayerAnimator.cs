using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    CharController boob;

    bool hasSwung;
    Coroutine swingCoroutine;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        boob = GetComponent<CharController>();
    }

    void Start()
    {
        anim.SetInteger("State", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //current player state (atk, def, utl)
        StanceState stance = boob.currentStanceState;

        //check if left controller stick is moved
        if (boob.controllerInputLeftStick != Vector2.zero)
        {
            anim.SetInteger("State", 0);
        }
        else
        {
            anim.SetInteger("State", (int)stance + 1);
        }
        
        //cool guy version
        //anim.SetInteger("State", boob.controllerInputLeftStick != Vector2.zero ? 0 : (int)boob.currentStanceState + 1);
    }

    public void Swing()
    {
        hasSwung = true;

        //start the attack combo
        if (swingCoroutine == null)
        {
            swingCoroutine = StartCoroutine(AttackCombo());
        }
    }

    IEnumerator AttackCombo()
    {
        anim.SetTrigger("AttackSword1");

        float clipLength = -1;
        float clip2Length = -1;
        foreach (AnimationClip c in anim.runtimeAnimatorController.animationClips)
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

        float timer = clipLength * 0.75f;
        while (timer > 0)
        {
            hasSwung = false;
            anim.SetBool("AttackSword2", hasSwung);

            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        bool swinging = false;
        float hitWindow = clipLength * 0.25f;
        while (hitWindow > 0)
        {
            if (hasSwung)
            {
                swinging = true;
                print("swing");
                anim.SetBool("AttackSword2", hasSwung);
            }

            hitWindow -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //wait longer if true
        if (swinging)
        {
            yield return new WaitForSeconds(clip2Length);
        }

        hasSwung = false;
        if(!swinging) anim.SetBool("AttackSword2", hasSwung);
        swingCoroutine = null;
    }
}
