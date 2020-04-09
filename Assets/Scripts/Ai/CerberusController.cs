using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CerberusController : MonoBehaviour
{
    GameObject player;

    Vector3 playerLocation;

    float distanceToPlayer;

    float normalBiteDmg = 25.0f;
    float tripleHeadBiteDmg = 50.0f;
    float fireEyeBeamDmg = 10.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerLocation = player.transform.position;

        distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(playerLocation.x, 0, playerLocation.z));
    }
}