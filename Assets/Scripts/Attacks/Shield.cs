using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject enemy;

    public bool stunAttack;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontriggerenter");
        if (other.CompareTag("Enemy"))
        {
            enemy = other.gameObject;
            Debug.Log(enemy);
        }
    }

    public void StunAttack()
    {
        enemy.GetComponent<AiController>().stunned = true;
    }
}