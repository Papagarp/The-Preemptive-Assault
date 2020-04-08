using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatformAnim : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("isRisen", false);
    }

    public void weightOn()
    {
        anim.SetBool("isRisen", true);
    }

    public void weightOff()
    {
        anim.SetBool("isRisen", false);
    }
}
