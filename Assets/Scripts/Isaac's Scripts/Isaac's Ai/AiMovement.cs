using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;

public class AiMovement : MonoBehaviour
{
    public GameObject[] targets;

    public GameObject weapon;

    GameObject player;
    
    NavMeshAgent nav;

    public bool foundPlayer = false;

    public float shootDistance = 1.5f;
    public float runningSpeed = 10f;
    public float walkingSpeed = 3.5f;
    public float firingSpeed = 1f;
    public float reloadTime = 3.0f;

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