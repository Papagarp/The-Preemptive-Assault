using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateKey : MonoBehaviour
{

    public GameObject scriptHolder;
    OpenBossGate unlockGate;

    private void Start()
    {
        unlockGate = scriptHolder.GetComponent<OpenBossGate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("it triggers doesn't call");
            unlockGate.Unlock();
            Destroy(gameObject);
        }
    }
}
