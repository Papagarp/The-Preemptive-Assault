using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    CharController characterScript;

    public GameObject player;

    private void Start()
    {
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