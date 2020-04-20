using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Rotate : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    bool IsRotated = false;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("IsRotated", false);
    }

    public void SLRotate()
    {
        if (!IsRotated)
        {
            anim.SetBool("IsRotated", true);
            IsRotated = true;
            Debug.Log("IsRotated set true");
        }
        else
        {
            anim.SetBool("IsRotated", false);
            IsRotated = false;
            Debug.Log("IsRotated set false");
        }
    }
}
