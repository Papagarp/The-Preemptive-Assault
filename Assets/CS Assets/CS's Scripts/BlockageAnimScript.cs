using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockageAnimScript : MonoBehaviour
{

    public Animator anim;

    void Start()
    {
        anim.SetBool("BlockOn", false);
        anim.SetBool("MoveBlockage", false);
        anim.SetBool("ResetBlockage", false); 
    }

    public void weightOn()
    {
        anim.SetBool("MoveBlockage", true);
        anim.SetBool("ResetBlockage", false);
        anim.SetBool("BlockOn", true);
    }

    public void weightOff()
    {
        anim.SetBool("MoveBlockage", false);
        anim.SetBool("ResetBlockage", true);
        anim.SetBool("BlockOn", false);
    }

}
