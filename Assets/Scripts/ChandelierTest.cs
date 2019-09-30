using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierTest : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.AddComponent<Rigidbody>();

        if(gameObject.CompareTag("Chandelier"))
        {
            Destroy(gameObject);
        }
    }

    

}
