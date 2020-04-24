using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantDoorAnimScript : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("OpenDoorsOutwards", false);
        anim.SetBool("OpenDoorsInwards", false);
    }

    public void DoorInward()
    {
        Debug.Log("door inward being called");
        anim.SetBool("OpenDoorsInwards", true);
    }

    public void DoorOutward()
    {
        anim.SetBool("OpenDoorsOutwards", true);
    }

    public void DoorClose()
    {
        anim.SetBool("OpenDoorsOutwards", false);
        anim.SetBool("OpenDoorsInwards", false);
    }
}
