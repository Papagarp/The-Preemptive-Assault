using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitScript : MonoBehaviour
{

    public GameObject giantDoors;
    public GameObject insideDoorTrigger;
    public GameObject outsideDoorTrigger;

    GiantDoorAnimScript animationController;

    private void Start()
    {
        animationController = giantDoors.GetComponent<GiantDoorAnimScript>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.DoorClose();
            insideDoorTrigger.SetActive(true);
            outsideDoorTrigger.SetActive(true);
        }
    }

}
