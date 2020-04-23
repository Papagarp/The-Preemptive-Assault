using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideDoorScript : MonoBehaviour
{

    public GameObject outsideDoorTrigger;
    public GameObject giantDoors;

    GiantDoorAnimScript animationController;

    private void Start()
    {
        animationController = giantDoors.GetComponent<GiantDoorAnimScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.DoorOutward();
            outsideDoorTrigger.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            outsideDoorTrigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
