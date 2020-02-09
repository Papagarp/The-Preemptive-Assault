using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    CharacterScript characterScript;

    public GameObject player;

    bool inRange;

    private void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
    }

    private void Update()
    {
        if (inRange == true && characterScript.interact == true)
        {

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
