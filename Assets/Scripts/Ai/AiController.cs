using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum aiState
{
    MELEE,
    RANGED,
    SEARCH,
    PATROL
}

public class AiController : MonoBehaviour
{
    #region Var
    
    //nav mesh agent
    public NavMeshAgent nav;

    //animation
    Animator aiAnimatorComponent;
    Coroutine swingCoroutine;
    bool hasSwung;
    
    public aiState currentAIState;

    [Header("Assign Script")]
    public Bolt boltScript;

    [Header("Assign GameObjects")]
    public GameObject crossbow;
    public GameObject crossbowBolt;
    
    [Header("Is this a patrolling Ai")]
    public bool patrollingAI;

    [Header("Assign Patrol Points")]
    public GameObject[] patrolPoints;
    int currentPoint = 0;

    //starting point
    Vector3 startAiPoint;
    Quaternion startAiRotation;

    [Header("Searching for player")]
    GameObject player;
    CharController playerScript;
    public bool foundPlayer;
    public bool foundPlayerCheck;
    public float distanceToPlayer;
    public float distanceToPoint;
    public float distanceToLastKnown;
    public float searchTime = 3.0f;
    public Vector3 lastKnownPosition;

    [Header("Speed")]
    public float patrollingMovementSpeed = 3.0f;
    public float firingMovementSpeed = 1.0f;
    public float staggerMovementSpeed = 0.1f;
    public float attackingMovementSpeed = 10.0f;

    [Header("other")]
    public bool stagger;
    public float stunRange = 10.0f;

    public float meleeRange = 5.0f;
    public float reloadTime = 3.0f;
    public float staggerTime = 3.0f;
    public float stunnedTime = 0.0f;

    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiAnimatorComponent = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<CharController>();
        boltScript = crossbowBolt.GetComponent<Bolt>();
    }

    private void Start()
    {
        //setting the guard's station point
        if (!patrollingAI)
        {
            startAiPoint = gameObject.transform.position;
            startAiRotation = gameObject.transform.rotation;
        }
    }

    private void Update()
    {
        #region state switching

        //TODO: Clean up state machine of useless code
        switch (currentAIState)
        {
            case (aiState.MELEE):
                crossbow.SetActive(false);
                nav.speed = attackingMovementSpeed;
                break;

            case (aiState.RANGED):
                crossbow.SetActive(true);
                nav.speed = firingMovementSpeed;
                break;

            case (aiState.SEARCH):
                nav.speed = patrollingMovementSpeed;
                break;

            case (aiState.PATROL):
                nav.speed = patrollingMovementSpeed;
                break;
        }

        #endregion

        #region Animation

        aiAnimatorComponent.SetBool("Moving", nav.remainingDistance > 1 ? true : false);

        #endregion

        #region Stun Function

        if (distanceToPlayer < stunRange && playerScript.stunned)
        {
            nav.speed = 0.0f;
        }

        #endregion

        #region Stagger Function

        if (stagger)
        {
            nav.speed = staggerMovementSpeed;

            staggerTime -= Time.deltaTime;

            if (staggerTime <= 0)
            {
                stagger = false;
            }
        }

        #endregion

        #region Patrolling & Search Function & reseting AI position

        if (!foundPlayer)
        {
            if (foundPlayerCheck)
            {
                currentAIState = aiState.SEARCH;
            }
            else
            {
                currentAIState = aiState.PATROL;
            }

            //if the player was lost then look at the last spot the player was seen
            if (currentAIState == aiState.SEARCH)
            {
                nav.SetDestination(lastKnownPosition);

                distanceToLastKnown = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(lastKnownPosition.x, 0, lastKnownPosition.z));

                //if at the last known spot of the player then search for a few seconds
                if(distanceToLastKnown < 0.1f)
                {
                    searchTime -= Time.deltaTime;

                    //if cannot find player then return to patrol
                    if (searchTime <= 0)
                    {
                        currentAIState = aiState.PATROL;
                        foundPlayerCheck = false;
                    }
                }
            }
            
            //if the player hasn't been seen then patrol/guard an area
            if(currentAIState == aiState.PATROL)
            {
                //is this Ai meant to patrol??
                if (patrollingAI)
                {
                    nav.SetDestination(patrolPoints[currentPoint].transform.position);

                    distanceToPoint = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(patrolPoints[currentPoint].transform.position.x, 0, patrolPoints[currentPoint].transform.position.z));

                    //move between patrol points
                    if (currentPoint == (patrolPoints.Length - 1) && distanceToPoint <= 0.1f)
                    {
                        currentPoint = 0;
                        nav.SetDestination(patrolPoints[currentPoint].transform.position);
                    }
                    else if (distanceToPoint <= 0.1f)
                    {
                        currentPoint++;
                        nav.SetDestination(patrolPoints[currentPoint].transform.position);
                    }
                }
                //if not a patrolling ai then its a guarding ai
                else
                {
                    nav.SetDestination(startAiPoint);

                    float distanceToStart = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                        new Vector3(startAiPoint.x, 0, startAiPoint.z));

                    if (distanceToStart < 1.0f)
                    {
                        gameObject.transform.rotation = startAiRotation;
                    }
                }
            }
        }

        #endregion

        #region Attacking the player

        if (foundPlayer)
        {
            foundPlayerCheck = true;
            searchTime = 3.0f;

            nav.SetDestination(player.transform.position);

            lastKnownPosition = player.transform.position;

            distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(player.transform.position.x, 0, player.transform.position.z));

            if (distanceToPlayer <= meleeRange)
            {
                currentAIState = aiState.MELEE;

                if (distanceToPlayer < 2.0f)
                {
                    gameObject.transform.LookAt(player.transform);
                    Vector3 currentLocation = gameObject.transform.position;
                    nav.SetDestination(currentLocation);
                    MeleeAttack();
                }
            }
            else if (distanceToPlayer >= meleeRange)
            {
                currentAIState = aiState.RANGED;

                if (!boltScript.fired)
                {
                    reloadTime -= Time.deltaTime;
                    if (reloadTime < 0)
                    {
                        boltScript.fired = true;
                    }
                }
            }
        }

        #endregion
    }

    void MeleeAttack()
    {
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