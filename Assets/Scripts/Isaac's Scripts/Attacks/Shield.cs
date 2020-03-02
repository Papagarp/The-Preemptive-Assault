using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;

    public bool stunAttack;

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<CharController>().canStun = true;

        if (stunAttack)
        {
            other.GetComponentInParent<AiController>().stunned = true;
            Debug.Log("stunned attack");
            stunAttack = false;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<CharController>().canStun = false;
    }
}