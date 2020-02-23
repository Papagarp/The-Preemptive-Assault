using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class AiController : MonoBehaviour
{
    public enum aiState
    {
        MELEE,
        RANGED,
        SEARCH,
        PATROL
    }

    public aiState currentAIState;

    public Bolt boltScript;

    public GameObject crossbow;
    public GameObject crossbowBolt;
    public GameObject player;
    public GameObject[] patrolPoints;

    public bool foundPlayer;
    public bool patrollingAI;

    public float distanceToPlayer;
    public float distanceToPoint;
    public float distanceToLastKnown;
    public float meleeRange = 5.0f;
    public float patrollingMovementSpeed = 3.0f;
    public float firingMovementSpeed = 0.2f;
    public float attackingMovementSpeed = 10.0f;
    public float searchTime = 3.0f;
    public float reloadTime = 3.0f;

    int i = 0;
    int foundPlayerCheck = 0;

    public Vector3 lastKnownPosition;
    public Vector3 startAiPoint;
    public Quaternion startAiRotation;

    NavMeshAgent nav;

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
        //state switching

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

        //------------------------------------------------------------------------------------------------------------------------------------

        //Patrolling Function & reseting AI position

        if (!foundPlayer)
        {
            if (foundPlayerCheck == 1)
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
                        foundPlayerCheck = 0;
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

        //------------------------------------------------------------------------------------------------------------------------------------

        //Attacking the player

        if (foundPlayer)
        {
            foundPlayerCheck = 1;
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
                        ShootAtPlayer();
                    }
                }
            }
        }
    }

    void ShootAtPlayer()
    {
        crossbowBolt.GetComponent<Bolt>().fired = true;
    }

    void MeleeAttack()
    {

    }
}
