using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossGate : MonoBehaviour
{

    public GameObject BossGate;
    
    //[SerializeField]
    public int currentLocks;
    int maxLocks = 3;
    int key = 1;

    private void Start()
    {
        currentLocks = maxLocks;
    }

    public void Unlock()
    {
        Debug.Log("Unlocks works");
        currentLocks -= key;
        Debug.Log("currentLocks = " + currentLocks);

        if(currentLocks == 0)
        {
            Debug.Log("currentLocks Destroyed");
            Destroy(BossGate);
        }
    }
}
