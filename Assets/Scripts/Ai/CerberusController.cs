using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CerberusController : MonoBehaviour
{
    //animation
    Coroutine normalBiteCo;
    Coroutine tripleBiteCo;
    Coroutine fireEyeBeamCo;
    Animator cerberusAnim;

    //nav mesh
    NavMeshAgent nav;

    //player
    GameObject player;
    CharController playerScript;
    Vector3 playerLocation;
    float distanceToPlayer;

    //enviroment
    float roomSize;
    Vector3 startingPosition;
    Quaternion startingRotation;

    //attacks
    bool isAttacking;
    float attackTimer = 0.1f;
    float normalBiteDmg = 25.0f;
    float tripleHeadBiteDmg = 50.0f;
    float fireEyeBeamDmg = 10.0f;

    private void Awake()
    {
        cerberusAnim = GetComponentInChildren<Animator>();

        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharController>();
    }

    private void Start()
    {
        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;
    }

    private void Update()
    {
        playerLocation = player.transform.position;

        distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), 
                           new Vector3(playerLocation.x, 0, playerLocation.z));

        if (distanceToPlayer < roomSize && !isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                int i = Random.Range(0, 2);

                if (i == 0)
                {
                    NormalBite();
                }
                else if (i == 1)
                {
                    TripleHeadBite();
                }
                else if (i == 2)
                {
                    FireEyeBeam();
                }

                isAttacking = true;
            }
        }
    }

    public void NormalBite()
    {
        if (distanceToPlayer < 2.0f)
        {
            if (normalBiteCo == null) normalBiteCo = StartCoroutine(NormalBiteAnim());
        }
        else
        {
            nav.SetDestination(player.transform.position);
        }
    }

    IEnumerator NormalBiteAnim()
    {
        cerberusAnim.SetTrigger("");

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "")
            {
                clipLength = c.length;
            }
        }

        yield return new WaitForSeconds(clipLength);

        isAttacking = false;
    }

    public void TripleHeadBite()
    {
        if (distanceToPlayer < 2.0f)
        {
            if (tripleBiteCo == null) tripleBiteCo = StartCoroutine(TripleBiteAnim());
        }
        else
        {
            nav.SetDestination(player.transform.position);
        }
    }

    IEnumerator TripleBiteAnim()
    {
        cerberusAnim.SetTrigger("");

        float clipLength = -1;
        foreach (AnimationClip c in cerberusAnim.runtimeAnimatorController.animationClips)
        {
            if (c.name == "")
            {
                clipLength = c.length;
            }
        }

        yield return new WaitForSeconds(clipLength);

        isAttacking = false;
    }

    public void FireEyeBeam()
    {

    }
}