using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Lever : MonoBehaviour
{
    public GameObject animatedObject;
    P2Rotate animationController;

    private void Start()
    {
        //anim = GetComponent<Animator>();

        animationController = animatedObject.GetComponent<P2Rotate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animationController.SLRotate();
            Debug.Log("player Triggered lever");
        }
    }
}
