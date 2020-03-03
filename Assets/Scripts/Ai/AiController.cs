using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    NavMeshAgent nav;

    public enum aiState
    {
        MELEE,
        RANGED,
        SEARCH,
        PATROL
    }

    public aiState currentAIState;

    [Header("Assign Script")]
    public Bolt boltScript;

    [Header("Assign GameObjects")]
    public GameObject crossbow;
    public GameObject crossbowBolt;
    public GameObject player;

    [Header("Is this a patrolling Ai")]
    public bool patrollingAI;

    [Header("Assign Patrol Points")]
    public GameObject[] patrolPoints;
    int i = 0;

    [Header("Start Point")]
    public Vector3 startAiPoint;
    public Quaternion startAiRotation;

    [Header("Searching for player")]
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
    public bool stunned;

    public float meleeRange = 5.0f;
    public float reloadTime = 3.0f;
    public float staggerTime = 3.0f;
    public float stunnedTime = 0.0f;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        boltScript = crossbowBolt.GetComponent<Bolt>();

        if (!patrollingAI)
        {
            startAiPoint = gameObject.transform.position;
            startAiRotation = gameObject.transform.rotation;
        }
    }

    private void Update()
    {
        #region state switching

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

        #region Stun Function

        if (stunned)
        {
            nav.speed = 0.0f;

            /*stunnedTime -= Time.deltaTime;

            if (stunnedTime <= 0)
            {
                stunned = false;
            }*/
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

            if (currentAIState == aiState.SEARCH)
            {
                nav.SetDestination(lastKnownPosition);

                distanceToLastKnown = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(lastKnownPosition.x, 0, lastKnownPosition.z));

                if(distanceToLastKnown < 0.1f)
                {
                    searchTime -= Time.deltaTime;

                    if (searchTime <= 0)
                    {
                        currentAIState = aiState.PATROL;
                        foundPlayerCheck = false;
                    }
                }
            }
            
            if(currentAIState == aiState.PATROL)
            {
                if (patrollingAI)
                {
                    nav.SetDestination(patrolPoints[i].transform.position);

                    distanceToPoint = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(patrolPoints[i].transform.position.x, 0, patrolPoints[i].transform.position.z));

                    if (i == (patrolPoints.Length - 1) && distanceToPoint <= 0.1f)
                    {
                        i = 0;
                        nav.SetDestination(patrolPoints[i].transform.position);
                    }
                    else if (distanceToPoint <= 0.1f)
                    {
                        i++;
                        nav.SetDestination(patrolPoints[i].transform.position);
                    }
                }
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

    }
}