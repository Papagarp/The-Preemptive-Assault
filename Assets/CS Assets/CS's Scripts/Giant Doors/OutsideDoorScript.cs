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
        Debug.Log("outDoor onTrig Works");
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("if works");
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
