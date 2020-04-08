using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoorAnimScript : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    bool isRotated = false;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isRotated", false);
    }

    public void swapLever()
    {
        if (!isRotated)
        {
            anim.SetBool("isRotated", true);
            isRotated = true;
            //Debug.Log("TDOoen set true");
        }
        else
        {
            anim.SetBool("isRotated", false);
            isRotated = false;
            //Debug.Log("TDOoen set false");
        }
    }
}
