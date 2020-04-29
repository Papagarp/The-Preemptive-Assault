using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CerberusController : MonoBehaviour
{
    #region Var

    //coroutines
    Coroutine normalBiteCo;
    Coroutine tripleBiteCo;
    Coroutine fireBallCo;
    Coroutine deathCo;

    //animation
    Animator cerberusAnim;

    //nav mesh
    NavMeshAgent nav;

    //player
    GameObject player;
    CharController playerScript;
    public Vector3 playerLocation;
    public float distanceToPlayer;

    //attacks
    CerberusFireBall fireBallScript;
    bool isAttacking;
    int attackCounter;
    float attackTimer = 0.1f;
    float normalBiteDmg = 25.0f;
    float tripleBiteDmg = 50.0f;

    //health
    float maxHp = 1000;
    public float currentHp = 1000;
    public bool startFight = false;

    #endregion

    private void Awake()
    {
        cerberusAnim = GetComponentInChildren<Animator>();

        fireBallScript = GetComponent<CerberusFireBall>();

        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharController>();
    }

    private void Update()
    {
        //death
        if (currentHp <= 0)
        {
            Death();
        }
        else if (startFight)
        {
            playerLocation = player.transform.position;

            distanceToPlayer = Vector3.Distance(transform.position, playerLocation);

            cerberusAnim.SetBool("Moving", nav.remainingDistance > 0.1 ? true : false);

            #region Choose the attack

            //every few seconds cerberus attacks. the attacks are random

            if (!isAttacking)
            {
                nav.isStopped = false;
                nav.SetDestination(playerLocation);

                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    if (normalBiteCo == null && tripleBiteCo == null && fireBallCo == null)
                    {
                        if (attackCounter == 2)
                        {
                            nav.isStopped = true;
                            FireBall();
                        }
                        else
                        {
                            int i = Random.Range(0, 2);

                            if (i == 0)
                            {
                                NormalBite();
                            }
                            else if (i == 1)
                            {
                                TripleBite();
                            }
                        }
                    }
                }
            }

            #endregion
        }
    }

    public void TakeDmg(float damage)
    {
        currentHp = currentHp - damage;
    }

    public void NormalBite()
    {
        if (nav.remainingDistance < 10.0f)
        {
            nav.isStopped = true;
            nav.SetDestination(transform.position);

            if (normalBiteCo == null) normalBiteCo = StartCoroutine(NormalBiteAnim());
        }
    }

    IEnumerator NormalBiteAnim()
    {
        cerberusAnim.SetTrigger("Bite");

        isAttacking = true;

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "biteAggressive")
            {
                clipLength = c.length;
            }
        }
        
        yield return new WaitForSeconds(clipLength);

        playerScript.TakeDmg(normalBiteDmg);
        attackTimer = 3.0f;
        isAttacking = false;
        attackCounter++;

        normalBiteCo = null;
    }

    public void TripleBite()
    {
        if (nav.remainingDistance < 10.0f)
        {
            nav.isStopped = true;
            nav.SetDestination(transform.position);

            if (tripleBiteCo == null) tripleBiteCo = StartCoroutine(TripleBiteAnim());
        }
    }

    IEnumerator TripleBiteAnim()
    {
        cerberusAnim.SetTrigger("JumpingBite");

        isAttacking = true;

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "jumpBiteNormal")
            {
                clipLength = c.length;
            }
        }
        
        yield return new WaitForSeconds(clipLength);

        playerScript.TakeDmg(tripleBiteDmg);
        attackTimer = 3.0f;
        isAttacking = false;
        attackCounter++;

        tripleBiteCo = null;
    }

    public void FireBall()
    {
        if (nav.remainingDistance > 10.0f)
        {
            nav.isStopped = true;
            nav.SetDestination(transform.position);

            if (fireBallCo == null) fireBallCo = StartCoroutine(FireBallAnim());
        }
    }

    IEnumerator FireBallAnim()
    {
        cerberusAnim.SetTrigger("BlowFire");

        isAttacking = true;

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "blowFireAggressive")
            {
                clipLength = c.length;
            }
        }

        yield return new WaitForSeconds(1.1f);

        fireBallScript.leftHeadFired = true;

        yield return new WaitForSeconds(0.8f);

        fireBallScript.middleHeadFired = true;

        yield return new WaitForSeconds(0.4f);

        fireBallScript.rightHeadFired = true;


        attackTimer = 3.0f;
        isAttacking = false;
        attackCounter = 0;

        nav.isStopped = false;

        fireBallCo = null;
    }

    public void Death()
    {
        if (deathCo == null) deathCo = StartCoroutine(DeathAnim());

        startFight = false;

        nav.isStopped = true;
    }

    IEnumerator DeathAnim()
    {
        cerberusAnim.SetTrigger("Die");

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "deathNormal")
            {
                clipLength = c.length;
            }
        }

        yield return new WaitForSeconds(clipLength);
    }
}