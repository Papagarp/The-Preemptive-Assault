using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject enemy;

    public bool stunAttack;

    private void Update()
    {
        if (stunAttack)
        {
            enemy.GetComponentInParent<AiController>().stunned = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AiController>())
        {
            gameObject.GetComponentInParent<CharController>().canStun = true;
            enemy = other.gameObject;
        }
    }
}