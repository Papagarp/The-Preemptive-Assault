using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetection : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            player.GetComponent<CharController>().hooked = true;
            player.GetComponent<CharController>().hookedObject = other.gameObject;
        }
    }
}
