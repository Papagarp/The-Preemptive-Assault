using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantDoorAnimScript : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("OpenDoorsOutward", false);
        anim.SetBool("OpenDoorsInward", false);
    }

    public void DoorInward()
    {
        anim.SetBool("OpenDoorsInward", true);
    }

    public void DoorOutward()
    {
        anim.SetBool("OpenDoorsOutward", true);
    }

    public void DoorClose()
    {
        anim.SetBool("OpenDoorsOutward", false);
        anim.SetBool("OpenDoorsInward", false);
    }
}
