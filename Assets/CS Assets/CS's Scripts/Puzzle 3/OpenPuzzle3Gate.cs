using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle3Gate : MonoBehaviour
{

    public GameObject cageDoor;

    [SerializeField]
    int currentL;
    int maxL = 3;
    int keyUnlock = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentL = maxL;
    }

    public void OpenDoor()
    {

        Debug.Log("currentLocks -= key; not working");
        currentL -= keyUnlock;
        Debug.Log("currentLocks = " + currentL);

        if (currentL == 0)
        {
            Debug.Log("currentLocks Destroyed");
            cageDoor.SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void CloseDoor()
    {
        currentL += keyUnlock;
        Debug.Log("currentLocks = " + currentL);

        if (currentL >= 0)
        {
            Debug.Log("currentLocks Destroyed");
            cageDoor.SetActive(true);
        }
        else
        {
            return;
        }
    }

}
