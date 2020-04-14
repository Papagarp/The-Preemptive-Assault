using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Start : MonoBehaviour
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
            Destroy(gameObject);
        }
    }
}
