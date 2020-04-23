using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoorScript : MonoBehaviour
{

    public GameObject insideDoorTrigger;
    public GameObject giantDoors;

    GiantDoorAnimScript animationController;

    void Start()
    {
        animationController = giantDoors.GetComponent<GiantDoorAnimScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            animationController.DoorInward();
            insideDoorTrigger.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        insideDoorTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
