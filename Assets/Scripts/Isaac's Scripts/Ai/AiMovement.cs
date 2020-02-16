using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class AiMovement : MonoBehaviour
{
    //NPBehave debugger


    public GameObject[] patrolPoints;

    public GameObject weapon;

    GameObject player;
    
    NavMeshAgent nav;

    public enum attackState
    {
        MELEE,
        RANGED
    }

    public attackState currentAttackState;

    public bool melee;
    public bool ranged;

    float distanceToPlayer;
    float distanceToTarget;

    public bool foundPlayer = false;

    public float shootDistance = 1.5f;
    public float runningSpeed = 10f;
    public float walkingSpeed = 3.5f;
    public float firingSpeed = 1f;
    public float reloadTime = 3.0f;

    int i = 0;

    void Start()
    {
        if (melee)
        {
            currentAttackState = attackState.MELEE;
        }
        else if (ranged)
        {
            currentAttackState = attackState.RANGED;
        }

        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        nav.SetDestination(patrolPoints[i].transform.position);
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(patrolPoints[i].transform.position.x, 0, patrolPoints[i].transform.position.z));
        distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(player.transform.position.x, 0, player.transform.position.z));

        if (i == (patrolPoints.Length - 1) && distanceToTarget <= 0.1f)
        {
            i = 0;
            nav.SetDestination(patrolPoints[i].transform.position);
        }
        else if (distanceToTarget <= 0.1f)
        {
            i++;
            nav.SetDestination(patrolPoints[i].transform.position);
        }

        switch (currentAttackState)
        {
            case (attackState.MELEE):

                if (foundPlayer)
                {
                    if(distanceToPlayer >= 3.0f)
                    {
                        nav.SetDestination(player.transform.position);
                        nav.speed = runningSpeed;
                    }
                    //else
                    //{
                    //    nav.isStopped = true;
                    //}
                }
                break;

            case (attackState.RANGED):
                
                break;
        }

        if (foundPlayer == true)
        {
            nav.SetDestination(player.transform.position);
            nav.speed = runningSpeed;
        }
        else if (foundPlayer == false)
        {
            nav.SetDestination(patrolPoints[i].transform.position);
            nav.speed = walkingSpeed;
        }

        if (distanceToPlayer <= shootDistance)
        {
            nav.speed = firingSpeed;
            reloadTime -= Time.deltaTime;

            if(reloadTime < 0)
            {
                Shoot();
            }
        }
        else
        {
            nav.speed = runningSpeed;
        }
    }

    void Shoot()
    {
        GameObject spear = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
        spear.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        reloadTime = 3;
    }
}