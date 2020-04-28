using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPressurePlateScript : MonoBehaviour
{

    public GameObject elevatorObject;
    ElevatorAnimScript animationController;

    private void Start()
    {
        animationController = elevatorObject.GetComponent<ElevatorAnimScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.PlayerOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.PlayerOff();
        }
    }
}
