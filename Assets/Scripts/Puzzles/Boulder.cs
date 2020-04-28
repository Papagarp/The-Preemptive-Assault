using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    //TODO: Update the Navmesh when moving the boulder

    CharController characterScript;

    GameObject player;

    GameObject currentObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterScript = player.GetComponent<CharController>();
    }

    private void Update()
    {
        if (currentObject != null)
        {
            if (!characterScript.holding)
            {
                currentObject.transform.parent = null;
            }

            if (characterScript.holding)
            {
                currentObject.transform.parent = player.transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Box"))
        {
            currentObject = other.gameObject;
        }
    }
}