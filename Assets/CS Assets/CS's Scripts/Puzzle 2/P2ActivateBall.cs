using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ActivateBall : MonoBehaviour
{

    public GameObject ballSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        ballSpawner.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ballSpawner.SetActive(true);
        }
    }
}
