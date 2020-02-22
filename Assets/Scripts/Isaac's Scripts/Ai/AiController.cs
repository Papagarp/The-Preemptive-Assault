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
        PATROL
    }

    public aiState currentAIState;

    public GameObject crossbowBolt;
    public GameObject player;
    public GameObject[] patrolPoints;

    public bool foundPlayer;
    public bool patrollingAI;

    public float distanceToPlayer;
    public float distanceToPoint;
    public float meleeRange = 5.0f;
    public float patrollingMovementSpeed = 3.0f;
    public float attackingMovementSpeed = 10.0f;
    public float reloadTime = 3.0f;

    int i = 0;

    public Vector3 startAiPoint;

    NavMeshAgent nav;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        if (patrollingAI)
        {
            nav.SetDestination(patrolPoints[i].transform.position);
        }
        else
        {
            startAiPoint = gameObject.transform.position;
        }
    }

    private void Update()
    {
        //state switching

        switch (currentAIState)
        {
            case (aiState.MELEE):
                nav.speed = attackingMovementSpeed;
                break;

            case (aiState.RANGED):
                reloadTime -= Time.deltaTime;
                break;

            case (aiState.PATROL):
                nav.speed = patrollingMovementSpeed;
                break;
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Patrolling Function

        if (patrollingAI && !foundPlayer)
        {
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
        else if (!foundPlayer)
        {
            nav.SetDestination(startAiPoint);
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        if (foundPlayer)
        {

            distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(player.transform.position.x, 0, player.transform.position.z));

            if (distanceToPlayer <= meleeRange)
            {
                currentAIState = aiState.MELEE;

                if (distanceToPlayer < 2.0f)
                {
                    Vector3 currentLocation = gameObject.transform.position;
                    nav.SetDestination(currentLocation);
                    MeleeAttack();
                }
                else
                {
                    nav.SetDestination(player.transform.position);
                }
                
            }
            else if (distanceToPlayer >= meleeRange)
            {
                currentAIState = aiState.RANGED;
                if (reloadTime < 0)
                {
                    ShootAtPlayer();
                }
            }
        }
    }

    void ShootAtPlayer()
    {
        
        //GameObject projectile = Instantiate(crossbowBolt, transform.position, Quaternion.identity) as GameObject;
        //projectile.transform.Translate(Vector3.forward * Time.deltaTime * 10.0f);
        reloadTime = 3;
    }

    void MeleeAttack()
    {

    }
}
