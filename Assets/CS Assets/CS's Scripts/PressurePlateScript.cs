using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{

    public GameObject animatedObject;
    BlockageAnimScript animationController;

    private void Start()
    {
        animationController = animatedObject.GetComponent<BlockageAnimScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Heavy")
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
