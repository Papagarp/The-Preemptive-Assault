using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2PressurePlate : MonoBehaviour
{
    public GameObject animatedObject;
    P2Blockage animationController;

    private void Start()
    {
        animationController = animatedObject.GetComponent<P2Blockage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("If statment entered");
            animationController.weightOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exited");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("If statment entered");
            animationController.weightOff();
        }
    }
}
