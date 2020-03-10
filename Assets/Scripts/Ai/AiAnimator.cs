using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAnimator : MonoBehaviour
{
    Animator aiAnimatorComponent;
    AiController aiController;
    Coroutine swingCoroutine;

    bool hasSwung;

    private void Awake()
    {
        aiAnimatorComponent = GetComponentInChildren<Animator>();
        aiController = GetComponent<AiController>();
    }

    private void Update()
    {
        aiAnimatorComponent.SetBool("Moving", aiController.nav.remainingDistance > 1 ? true : false);
    }

    public void Swing()
    {
        //start the attack combo
        if (swingCoroutine == null)
        {
            swingCoroutine = StartCoroutine(AttackCombo());
        }
    }

    IEnumerator AttackCombo()
    {
        print("BeginAttack");
        aiAnimatorComponent.SetTrigger("Claw1");

        float clipLength = -1;
        float clip2Length = -1;
        foreach (AnimationClip c in aiAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (c.name == "Claw1")
            {
                clipLength = c.length;
            }
            if (c.name == "Claw2")
            {
                clip2Length = c.length;
            }
        }

        //wait for first animation to end
        yield return new WaitForSeconds(clipLength);

        //check if still in range of player to perform second attack



        //bool swinging = false;
        //float hitWindow = clipLength * 0.25f;
        //while (hitWindow > 0)
        //{
        //    if (hasSwung)
        //    {
        //        swinging = true;
        //        print("swing");
        //        aiAnimatorComponent.SetBool("SecondAttack", hasSwung);
        //    }

        //    hitWindow -= Time.deltaTime;
        //    yield return new WaitForEndOfFrame();
        //}

        //wait longer if true
        //if (swinging)
        //{
        //    yield return new WaitForSeconds(clip2Length);
        //}

        swingCoroutine = null;
    }
}
