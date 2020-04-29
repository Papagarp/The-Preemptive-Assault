using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    CerberusController cerberusScript;

    GameObject cerberus;

    private void Awake()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");

        cerberusScript = cerberus.GetComponent<CerberusController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cerberusScript.startFight = true;
        }
    }
}