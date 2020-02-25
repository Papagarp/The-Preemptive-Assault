using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CerberusController : MonoBehaviour
{
    public enum cerberusState
    {
        WAITING,
        MELEE,
        RANGED
    }

    public cerberusState currentCerberusState;

    public GameObject player;

    public bool foundPlayer;

    public float distanceToPlayer;
    public float meleeRange;
    public float firingMovementSpeed = 3.0f;
    public float attackMovementSpeed = 10.0f;
    public float reloadTime = 3.0f;

    NavMeshAgent nav;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //state Switching

        switch (currentCerberusState)
        {
            case (cerberusState.MELEE):
                nav.speed = attackMovementSpeed;
                break;

            case (cerberusState.RANGED):
                nav.speed = firingMovementSpeed;
                break;

            case (cerberusState.WAITING):
                break;
        }

        if (foundPlayer)
        {
            Debug.Log("foundplayer");
        }
    }
}

