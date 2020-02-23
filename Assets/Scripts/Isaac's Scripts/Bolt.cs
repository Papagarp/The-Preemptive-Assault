using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public GameObject crossbow;

    public GameObject ai;

    public float boltFlyingTime = 5.0f;

    public bool fired = false;

    private void Update()
    {
        if (fired)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 100.0f);

            boltFlyingTime -= Time.deltaTime;

            if (boltFlyingTime < 0)
            {
                fired = false;
                ReturnBolt();
            }
        }
    }

    private void OntriggerEnter (Collider other)
    {
        Debug.Log(other.name);
        fired = false;
        ReturnBolt();
    }

    void ReturnBolt()
    {
        gameObject.transform.position = crossbow.transform.position;
        ai.GetComponent<AiController>().reloadTime = 3.0f;
        boltFlyingTime = 5.0f;
    }
}