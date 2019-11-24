using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class AiMovement : MonoBehaviour
{
    public GameObject[] targets;

    GameObject player;
    
    NavMeshAgent nav;

    public bool foundPlayer = false;

    public float killDistance = 1.5f;
    public float runningSpeed = 10;
    public float walkingSpeed = 3.5f;

    int i = 0;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        nav.SetDestination(targets[i].transform.position);
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targets[i].transform.position.x, 0, targets[i].transform.position.z));

        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));

        if (i == (targets.Length-1) && distanceToTarget <= 0.1f)
        {
            i = 0;
            nav.SetDestination(targets[i].transform.position);
        }
        else if (distanceToTarget <= 0.1f)
        {
            i++;
            nav.SetDestination(targets[i].transform.position);
        }
        
        if (foundPlayer == true)
        {
            nav.SetDestination(player.transform.position);
            nav.speed = runningSpeed;
        }
        else if (foundPlayer == false)
        {
            nav.SetDestination(targets[i].transform.position);
            nav.speed = walkingSpeed;
        }

        if (distanceToPlayer <= killDistance)
        {
            nav.SetDestination(transform.position);
        }
    }
}