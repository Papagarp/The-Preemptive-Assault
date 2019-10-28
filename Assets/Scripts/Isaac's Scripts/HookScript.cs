using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    CharacterScript characterScript;

    public GameObject player;

    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public bool hooked;
    public bool fired;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;

    private bool grounded;
    public bool flag;

    void Start()
    {
        characterScript = player.GetComponent<CharacterScript>();
    }

    private void Update()
    {
        hook.transform.localScale = new Vector3(1, 1, 1);

        CheckIfGround();

        if (characterScript.currentStanceState != CharacterScript.StanceState.UTILITY)
        {
            hookHolder.SetActive(false);
        }
        else
        {
            hookHolder.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1) && !fired && characterScript.currentStanceState == CharacterScript.StanceState.UTILITY)
        {
            fired = true;
        }

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();

            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);

            Invoke("RetractHook", 0.4f);
        }

        if (!fired)
        {
            flag = false;
        }
        
        if (fired && !hooked)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);

            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (hooked && fired)
        {
            hook.transform.parent = hookedObj.transform;

            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);

            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);
            this.GetComponent<Rigidbody>().useGravity = false;
            
            if (distanceToHook < 1)
            {
                if (!grounded)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 25f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 30f);
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void RetractHook()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ReturnHook();
        }
    }

    
    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    void ReturnHook()
    {
        
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;

        
        fired = false;
        hooked = false;

        
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }

    
    void CheckIfGround()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
