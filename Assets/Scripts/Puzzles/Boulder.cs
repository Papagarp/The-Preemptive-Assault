using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    CharController characterScript;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterScript = player.GetComponent<CharController>();
    }

    private void Update()
    {
        if (!characterScript.holding)
        {
            gameObject.transform.parent = null;
        }
        
        if (characterScript.holding)
        {
            gameObject.transform.parent = player.transform;
        }
    }
}