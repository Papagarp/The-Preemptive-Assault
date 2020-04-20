using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool spinAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AiController>())
        {
            if (spinAttack)
            {
                other.GetComponentInParent<AiController>().stagger = true;
                Debug.Log("spin attack");
            }
            else
            {
                //Debug.Log("basic attack");
            }
        }
    }
}