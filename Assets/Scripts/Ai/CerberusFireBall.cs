using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusFireBall : MonoBehaviour
{
    CerberusController cerberusScript;

    public GameObject anchorPointLeftHead;
    public GameObject anchorPointRightHead;
    public GameObject anchorPointMiddleHead;

    public GameObject fireBallLeft;
    public GameObject fireBallRight;
    public GameObject fireBallMiddle;

    float flyingTimeLeftHead = 5.0f;
    float flyingTimeRightHead = 5.0f;
    float flyingTimeMiddleHead = 5.0f;

    public bool leftHeadFired = false;
    public bool rightHeadFired = false;
    public bool middleHeadFired = false;

    float fireBallDmg = 20.0f;

    private void Awake()
    {
        cerberusScript = GetComponentInParent<CerberusController>();
    }

    private void Update()
    {
        if (leftHeadFired)
        {
            fireBallLeft.transform.parent = null;
            fireBallLeft.transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);

            flyingTimeLeftHead -= Time.deltaTime;

            if (flyingTimeLeftHead < 0)
            {
                leftHeadFired = false;
                ReturnLeftFireBall();
            }
        }

        if (rightHeadFired)
        {
            fireBallRight.transform.parent = null;
            fireBallRight.transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);

            flyingTimeRightHead -= Time.deltaTime;

            if (flyingTimeRightHead < 0)
            {
                rightHeadFired = false;
                ReturnRightFireBall();
            }
        }

        if (middleHeadFired)
        {
            fireBallMiddle.transform.parent = null;
            fireBallMiddle.transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);

            flyingTimeMiddleHead -= Time.deltaTime;

            if (flyingTimeMiddleHead < 0)
            {
                middleHeadFired = false;
                ReturnMiddleFireBall();
            }
        }
    }

    public void ReturnLeftFireBall()
    {
        //TODO:Cerberus Reload
        fireBallLeft.transform.position = anchorPointLeftHead.transform.position;
        fireBallLeft.transform.rotation = anchorPointLeftHead.transform.rotation;
        fireBallLeft.transform.parent = anchorPointLeftHead.transform;
        flyingTimeLeftHead = 5.0f;
    }

    public void ReturnRightFireBall()
    {
        //TODO:Cerberus Reload
        fireBallRight.transform.position = anchorPointRightHead.transform.position;
        fireBallRight.transform.rotation = anchorPointRightHead.transform.rotation;
        fireBallRight.transform.parent = anchorPointRightHead.transform;
        flyingTimeRightHead = 5.0f;
    }

    public void ReturnMiddleFireBall()
    {
        //TODO:Cerberus Reload
        fireBallMiddle.transform.position = anchorPointMiddleHead.transform.position;
        fireBallMiddle.transform.rotation = anchorPointMiddleHead.transform.rotation;
        fireBallMiddle.transform.parent = anchorPointMiddleHead.transform;
        flyingTimeMiddleHead = 5.0f;
    }
}
