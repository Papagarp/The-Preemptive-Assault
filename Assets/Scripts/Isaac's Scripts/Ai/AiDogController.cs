using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiDogController : MonoBehaviour
{
    public enum aiDogState
    {
        PATROL,
        SEARCH,
        ATTACK
    }

    public aiDogState currentAiDogState;

    public GameObject player;
    public GameObject[] patrolPoints;

    public bool foundPlayer;
    public bool foundPlayerCheck;

    public float distanceToPlayer;
    public float distanceToPoint;
    public float distanceToLastKnown;
    public float patrollingMovementSpeed = 3.0f;
    public float attackingMovementSpeed = 15.0f;
    public float searchTime = 5.0f;

    int i = 0;

    public Vector3 lastKnownPosition;

    NavMeshAgent nav;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //state switching

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

        //------------------------------------------------------------------------------------------------------------------------------------

        //Patrol and Search Function

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
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Attacking the player

        if (foundPlayer)
        {
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

        //------------------------------------------------------------------------------------------------------------------------------------
    }

    void BiteAttack()
    {

    }
}
