using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.Translate(0, -0.1f, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.Translate(0, 0.1f, 0);
    }
}
