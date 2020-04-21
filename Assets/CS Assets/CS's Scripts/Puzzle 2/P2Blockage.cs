using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Blockage : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("BlockUp", false);
    }

    public void weightOn()
    {
        anim.SetBool("BlockUp", true);
    }

    public void weightOff()
    {
        anim.SetBool("BlockUp", false);
    }
}
