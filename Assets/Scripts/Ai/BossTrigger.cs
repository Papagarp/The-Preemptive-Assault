using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    CerberusController cerberusScript;

    GameObject cerberus;

    JesseAudioManager jesseAudioManager;

    private void Awake()
    {
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");

        cerberusScript = cerberus.GetComponent<CerberusController>();
    }

    void Start()
    {
        jesseAudioManager = FindObjectOfType<JesseAudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cerberusScript.startFight = true;
            jesseAudioManager.StopSound("Music");
            jesseAudioManager.PlaySound("Boss Music");
        }
    }
}