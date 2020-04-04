using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockageAnimScript : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("OpenBlockage", false);
    }

    public void weightOn()
    {
        anim.SetBool("OpenBlockage", true);
    }

    public void weightOff()
    {
        anim.SetBool("OpenBlockage", false);
    }



    //Old Animations Script

    //void Start()
    //{
    //    anim.SetBool("BlockOn", false);
    //    anim.SetBool("MoveBlockage", false);
    //    anim.SetBool("ResetBlockage", false); 
    //}

    //public void weightOn()
    //{
    //    anim.SetBool("MoveBlockage", true);
    //    anim.SetBool("ResetBlockage", false);
    //    anim.SetBool("BlockOn", true);
    //}

    //public void weightOff()
    //{
    //    anim.SetBool("MoveBlockage", false);
    //    anim.SetBool("ResetBlockage", true);
    //    anim.SetBool("BlockOn", false);
    //}

}
