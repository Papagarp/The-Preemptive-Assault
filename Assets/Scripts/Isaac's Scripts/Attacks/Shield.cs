using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool stunAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AiController>())
        {
            if (stunAttack)
            {
                other.GetComponentInParent<AiController>().stunned = true;
                Debug.Log("stunned attack");
                stunAttack = false;
            }
        }
    }
}
