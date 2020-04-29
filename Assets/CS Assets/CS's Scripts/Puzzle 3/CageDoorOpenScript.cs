using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoorOpenScript : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim.SetBool("CDOpen", false);
    }

    public void OpenCage()
    {
        anim.SetBool("CDOpen", true);
    }

    public void CloseCage()
    {
        anim.SetBool("CDOpen", false);
    }
}
