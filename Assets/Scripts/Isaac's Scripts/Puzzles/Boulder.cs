using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    CharController characterScript;

    public GameObject player;

    float grab = 1.0f;

    bool inRange;
    bool holding;

    private void Start()
    {
        characterScript = player.GetComponent<CharController>();
    }

    private void Update()
    {
        grab -= Time.deltaTime;

        if(grab <= 0.0f)
        {
            if (holding && characterScript.interact)
            {
                gameObject.transform.parent = null;
                holding = false;
                grab = 1.0f;
            }
        }

        if (grab <= 0.0f)
        {
            if (inRange && characterScript.interact)
            {
                gameObject.transform.parent = player.transform;
                holding = true;
                grab = 1.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
