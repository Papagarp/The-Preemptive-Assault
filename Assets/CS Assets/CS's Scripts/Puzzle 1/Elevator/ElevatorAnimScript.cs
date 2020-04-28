using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAnimScript : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("ElevatorUp", false);
    }

    public void PlayerOn()
    {
        anim.SetBool("ElevatorUp", true);
    }

    public void PlayerOff()
    {
        anim.SetBool("ElevatorUp", false);
    }
}
