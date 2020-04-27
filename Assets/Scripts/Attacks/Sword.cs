using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    CharController playerScript;

    float swordDmg = 10.0f;

    public bool spinAttack;

    private void Awake()
    {
        playerScript = GetComponentInParent<CharController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AiController>() && playerScript.isAttacking)
        {
            if (spinAttack)
            {
                other.GetComponentInParent<AiController>().stagger = true;

            }
            else
            {
                other.GetComponentInParent<AiController>().TakeDmg(swordDmg);
            }
        }
        else if (other.GetComponentInParent<CerberusController>() && playerScript.isAttacking)
        {
            other.GetComponentInParent<CerberusController>().TakeDmg(swordDmg);
        }
    }
}