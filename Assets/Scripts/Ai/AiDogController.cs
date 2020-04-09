using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum aiDogState
{
    PATROL,
    SEARCH,
    ATTACK
}

//TODO: This entire script needs to be updated

public class AiDogController : MonoBehaviour
{
    #region Var

    //enum
    public aiDogState currentAiDogState;

    //animation
    Animator aiDogAnimatorComponent;

    //player
    GameObject player;
    CharController playerScript;
    public float distanceToPlayer;
    public float distanceToLastKnown;
    public bool foundPlayer;
    public bool foundPlayerCheck;
    public Vector3 lastKnownPosition;

    //nav mesh

    [Header("Is this a Patrolling Dog?")]
    public bool patrollingAiDog;

    //starting point
    Vector3 startAiDogPoint;
    Quaternion startAiDogRotation;

    public GameObject[] patrolPoints;
    NavMeshAgent nav;
    public float distanceToPoint;
    public float patrollingMovementSpeed = 3.0f;
    public float attackingMovementSpeed = 15.0f;
    public float searchTime = 5.0f;
    int currentPoint = 0;

    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharController>();

        aiDogAnimatorComponent = GetComponentInChildren<Animator>();
        
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (!patrollingAiDog)
        {
            startAiDogPoint = gameObject.transform.position;
            startAiDogRotation = gameObject.transform.rotation;
        }
    }

    private void Update()
    {
        #region state switching

        switch (currentAiDogState)
        {
            case (aiDogState.ATTACK):
                nav.speed = attackingMovementSpeed;
                break;

            case (aiDogState.PATROL):
                nav.speed = patrollingMovementSpeed;
                break;

            case (aiDogState.SEARCH):
                nav.speed = attackingMovementSpeed;
                break;
        }

        #endregion

        #region Animation

        aiDogAnimatorComponent.SetBool("Moving", nav.remainingDistance > 1 ? true : false);

        #endregion

        #region Patrol and Search Function

        if (!foundPlayer)
        {
            if (foundPlayerCheck)
            {
                currentAiDogState = aiDogState.SEARCH;
            }
            else
            {
                currentAiDogState = aiDogState.PATROL;
            }

            if(currentAiDogState == aiDogState.SEARCH)
            {
                nav.SetDestination(lastKnownPosition);

                distanceToLastKnown = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(lastKnownPosition.x, 0, lastKnownPosition.z));

                if (distanceToLastKnown < 0.1f)
                {
                    searchTime -= Time.deltaTime;

                    if (searchTime <= 0)
                    {
                        currentAiDogState = aiDogState.PATROL;
                        foundPlayerCheck = false;
                    }
                }
            }

            if(currentAiDogState == aiDogState.PATROL)
            {
                nav.SetDestination(patrolPoints[currentPoint].transform.position);

                distanceToPoint = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(patrolPoints[currentPoint].transform.position.x, 0, patrolPoints[currentPoint].transform.position.z));

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
        }

        #endregion

        #region Attacking the player

        if (foundPlayer)
        {
            currentAiDogState = aiDogState.ATTACK;

            foundPlayerCheck = true;
            searchTime = 3.0f;

            nav.SetDestination(player.transform.position);

            lastKnownPosition = player.transform.position;

            distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(player.transform.position.x, 0, player.transform.position.z));

            if (distanceToPlayer < 2.0f)
            {
                gameObject.transform.LookAt(player.transform);
                Vector3 currentLocation = gameObject.transform.position;
                nav.SetDestination(currentLocation);
                BiteAttack();
            }
        }

        #endregion
    }

    void BiteAttack()
    {

    }
}