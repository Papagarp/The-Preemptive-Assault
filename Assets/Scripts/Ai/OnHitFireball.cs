using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitFireball : MonoBehaviour
{
    GameObject player;

    CharController playerScript;

    float fireBallDmg = 20.0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<CharController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.TakeDmg(fireBallDmg);
        }
    }
}
