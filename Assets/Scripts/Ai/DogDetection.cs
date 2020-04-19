using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDetection : MonoBehaviour
{
    DogController DogControllerScript;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float distanceToPlayer;

    GameObject player;

    public LayerMask obstacleMask;
    public LayerMask playerMask;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);

        DogControllerScript = GetComponent<DogController>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > viewRadius)
        {
            DogControllerScript.foundPlayer = false;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                {
                    DogControllerScript.foundPlayer = true;
                }
                else if (disToTarget >= viewRadius)
                {
                    DogControllerScript.foundPlayer = false;
                }
            }
        }
    }

    public Vector3 directionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
