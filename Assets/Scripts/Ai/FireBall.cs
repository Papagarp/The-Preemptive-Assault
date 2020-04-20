using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    AiController aiScript;

    public GameObject anchorPoint;

    float flyingTime = 5.0f;

    public bool fired = false;

    private void Awake()
    {
        aiScript = GetComponentInParent<AiController>();
    }

    private void Update()
    {
        if (fired)
        {
            gameObject.transform.parent = null;
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);

            flyingTime -= Time.deltaTime;

            if (flyingTime < 0)
            {
                fired = false;
                ReturnFireBall();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ai")
        {
            fired = false;
            ReturnFireBall();
        }
    }

    public void ReturnFireBall()
    {
        aiScript.reloadTime = 3.0f;
        gameObject.transform.position = anchorPoint.transform.position;
        gameObject.transform.rotation = anchorPoint.transform.rotation;
        gameObject.transform.parent = anchorPoint.transform;
        flyingTime = 5.0f;
    }
}
