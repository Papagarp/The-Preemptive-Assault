using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject animatedObject;
    TrapDoorAnimScript animationController;

    private void Start()
    {
        //anim = GetComponent<Animator>();

        animationController = animatedObject.GetComponent<TrapDoorAnimScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.swapLever();
            Debug.Log("player Triggered lever");
        }
    }
}
