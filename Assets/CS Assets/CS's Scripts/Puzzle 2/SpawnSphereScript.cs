using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphereScript : MonoBehaviour
{
    public GameObject sphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P2Floor")
        {
            Destroy(sphere);
        }
    }
}
