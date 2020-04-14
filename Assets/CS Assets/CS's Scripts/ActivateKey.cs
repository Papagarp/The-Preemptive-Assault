using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateKey : MonoBehaviour
{
    OpenBossGate unlockGate;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            unlockGate.Unlock();
            Destroy(gameObject);
        }
    }
}
