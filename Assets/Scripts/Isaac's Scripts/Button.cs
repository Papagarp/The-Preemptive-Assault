using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    CharacterScript characterScript;

    public GameObject player;

    bool inRange;

    float buttonReset = 1.0f;

    private void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
    }

    private void Update()
    {
        buttonReset -= Time.deltaTime;
        if (inRange == true && characterScript.interact == true)
        {
            Debug.Log("open");
            //insert something opening desu
            Vector3 pushed = new Vector3(0, 1, 0.3f);
            gameObject.transform.position = pushed;
            buttonReset = 1.0f;
        }

        if (buttonReset < 0)
        { 
            Vector3 reset = new Vector3(0, 1 ,0.2f);
            gameObject.transform.position = reset;
            Debug.Log("reset");
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