using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
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

    private void Update()
    {
        //Constantly set the scale of the hook to 1,1,1
        //when the hook becomes a child of the obstacle it connected it with, the scale goes nuts, this is a temporary fix
        hook.transform.localScale = new Vector3(1, 1, 1);

        //Constantly check if the player is grounded. Used later during the script with variable 'grounded'
        CheckIfGround();

        //Get access to the script 'Player'
        Player playerScript = GetComponent<Player>();

        //If Utility stance is NOT on, make the grapple hook invisible
        if (!playerScript.utilityOn)
        {
            hookHolder.SetActive(false);
        }

        //If Utility stance IS on, make the grapple hook visible
        else
        {
            hookHolder.SetActive(true);
        }

        //If the player is:
            //holding right click 
            //Utility stance is ON
            //Have not already fired
            //Then they can fire the grapple (fired is set to true)
        if (Input.GetMouseButtonDown(1) && !fired && playerScript.utilityOn)
        {
            fired = true;
        }

        //If 'fired' is set to true
        if (fired)
        {
            //Get access to the linerenderer
            LineRenderer rope = hook.GetComponent<LineRenderer>();

            //Draw a line
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        //If the player's
            //Hook has NOT landed
            //Are currently firing
            //Then the grapple moves forward
        if (fired && !hooked)
        {
            //Moves the grapple in it's forward direction. Speed is based on variable 'hookTravelSpeed'
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);

            //Track the current distance the grapple has gone since it started moving
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            //If the hooks current distance goes past the max distance I want it too, make it return back (That's 'ReturnHook()')
            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        //If the player's
            //Hook has grabbed onto something
            //'fired' is true
        if (hooked && fired)
        {
            //Remove the hook's parent from the players, and onto the object it landed on.
            //This is so the player's position can be moved towards it easily. (I think)
            hook.transform.parent = hookedObj.transform;

            //Move the player's position towards the hook. The speed is based on variable 'playerTravelSpeed'
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);

            //Track the player's distance from themself, and the hook.
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            //Disable gravity, so the transition to the hook is smooth.
            this.GetComponent<Rigidbody>().useGravity = false;

            //If the player's distance to the hook is less then 1 unit in game
            if (distanceToHook < 1)
            {
                //If the player is not touching the ground
                if (!grounded)
                {
                    //Launch the player upwards and forwards, hoping they are placed on the top of platform they hooked to.
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 25f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 30f);

                }

                StartCoroutine("Climb");
            }
        }

        //If:
            //The player's hook has not landed on an object
            //The player hasn't shot the grapple yet
        else
        {
            //Make sure the hook is parented to the player
            hook.transform.parent = hookHolder.transform;
            
            //Make sure the player's gravity is on
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    //I don't know why this part is needed
    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    void ReturnHook()
    {
        //I think, the hook's rotation is reset back to it's default rotation, using the 'hookHolder' gameobject
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;

        //Set variables 'fired' and 'hooked' to false
        fired = false;
        hooked = false;

        //Kill the line that was made for the grapple
        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }

    //Checks if the player is grounded, if they are, variable 'grounded' = true.
    //If not variable 'grounded' = false
    void CheckIfGround()
    {
        //Create a Ray called 'hit'
        RaycastHit hit;
        //The distance of what this ray will be
        float distance = 1f;

        //Create the direction the ray will go in
        Vector3 dir = new Vector3(0, -1);

        //Shoot the ray in the variable 'dir' for direction
        //and for 1 unit, made in variable 'distance'
        //If the ray DOES touch anything, the player is not grounded
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }

        //If the ray DOES NOT touch anything, the player is not grounded
        else
        {
            grounded = false;
        }
    }
}
