using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateV2 : MonoBehaviour
{

    public GameObject animatedObject;
    RisingPlatformAnim animationController;

    private void Start()
    {
        animationController = animatedObject.GetComponent<RisingPlatformAnim>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Heavy")
        {
            animationController.weightOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Heavy")
        {
            animationController.weightOff();
        }
    }

}
