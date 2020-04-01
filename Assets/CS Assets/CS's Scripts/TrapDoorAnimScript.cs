using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorAnimScript : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    bool TDOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("OpenTD", false);
    }

    public void swapLever()
    {
        if (!TDOpen)
        {
            anim.SetBool("OpenTD", true);
            TDOpen = true;
            Debug.Log("TDOoen set true");
        }
        else
        {
            anim.SetBool("OpenTD", false);
            TDOpen = false;
            Debug.Log("TDOoen set false");
        }
    }
}
