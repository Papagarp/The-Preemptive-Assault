using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3Key : MonoBehaviour
{

    public GameObject ScriptHolder;
    OpenPuzzle3Gate Puzzle3Gate;

    private void Start()
    {
        Puzzle3Gate = ScriptHolder.GetComponent<OpenPuzzle3Gate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Heavy")
        {
            Debug.Log("OpenDoorActivated");
            Puzzle3Gate.OpenDoor();
            Debug.Log("OpenDoorActivated");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Heavy")
        {
            Debug.Log("CloseDoorActivated");
            Puzzle3Gate.CloseDoor();
            Debug.Log("CloseDoorActivated");
        }
    }
}
