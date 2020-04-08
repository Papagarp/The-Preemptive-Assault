using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1LeverScript : MonoBehaviour
{
    public GameObject animatedObject;
    RotatingDoorAnimScript animationController;

    private void Start()
    {
        //anim = GetComponent<Animator>();

        animationController = animatedObject.GetComponent<RotatingDoorAnimScript>();
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
