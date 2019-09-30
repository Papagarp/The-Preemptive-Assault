using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //------------------------------------------------------------------------------------------VARIABLES

    //The player's current speed value
    public float speed = 10f;
    //The player's base speed value
    public float baseSpeed = 10f;
    //The player's speed while in Utility stance
    public float utilitySpeed = 15f;
    //IDK
    public Vector3 jump;
    //The player's current damage
    public float damage = 5f;
    //The player's damage in Attack mode
    public float OffenceDamage = 10f;
    //The player's default damage value
    public float baseDamage = 5f;
    //The strength of the player's jump
    public float jumpStrength = 2f;
    //Amount of time that passes until heavier gravity kicks in after jumping
    public float jumpDownWait = .4f;
    //The intense gravity value
    public float gravityStr;
    //The regular gravity value
    public float gravityNor;
    //Whether or not the player has hit
    public bool hitIssued;

    //The current time the player is waiting for the hit Cooldown
    public float hitDuration;
    //
    public float hitCooldown = .4f;
    //
    public bool hitCooldownOver = true;
    //
    public bool justHit;
    //IF the player is holding the hit or not
    public bool holdingHit;
    //Can hit
    public bool canHit = true;
    //The player's current health
    public float health = 5;
    //The amount of damage the player takes
    public float healthDecAmount = 5f;
    //The amount of damage the player takes WHEN SHIELDING
    public float healthShieldDecAmount = 2f;


    //If the player is jumping
    public bool jumping = false;
    //If the player is shielding
    public bool isShielding;
    //If utility stance is on
    public bool utilityOn = true;
    //If Defence stance is on
    public bool defenceOn;
    //If Attack stance is on
    public bool attackOn;
    //If the player is hitting
    public bool hitting;


    //The shooter enemy
    public GameObject enemy1;
    //The player's healthbar
    public Image healthBar; 
    //The player's sword
    public GameObject sword;
    //The player's shield
    public GameObject shield;
    //The player (IDK why I made this, but it might be required somewhere down there
    public GameObject player;
    //The player's rigidbody (Also don't remember I did this)
    public Rigidbody rb;
    //The player's current material
    private Material playerMat;
    //The camera
    private GameObject cam;

    //------------------------------------------------------------------------------------------CODE


    //Upon start
    void Start()
    {
        //Find the camera
        cam = GameObject.FindWithTag("MainCamera");

        //Set the gravity of scene
        Physics.gravity = new Vector3(0, gravityNor, 0);

        //I don't remember what this is
        jump = new Vector3(0.0f, jumpStrength, 0.0f);

        //Set what variable 'playerMat' is
        playerMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Call functions every 'FixedFrame'
        PlayerMovement();
        PlayerJump();
        Shielding();
        StanceChange();
        StanceStats();
        PlayerAttack();
        CanPlayerAttack();

        SpawningEnemy();
        //HitSwingDuration();
        //HitCooldown();
    }

    //Spawning an enemy
    void SpawningEnemy()
    {
        //If player is pressing K
        if (Input.GetKey(KeyCode.K))
        {
            //Spawn enemy 1 at fixed location
            Instantiate(enemy1, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    void CanPlayerAttack()
    {
        if (!hitting && !isShielding)
        {
            canHit = true;
        }
        else
        {
            canHit = false;
        }
    }

    void PlayerAttack()
    {
        if (Input.GetMouseButton(0) && canHit && !hitIssued && hitCooldownOver)
        {
            StartCoroutine("Attacking");
            StartCoroutine("HitCooldown");

            hitIssued = true;
            hitCooldownOver = false;
        }
    }

    //Player Attacking with sword
    IEnumerator Attacking()
    {
        sword.transform.localRotation = Quaternion.Euler(90, 0, 0);

        yield return new WaitForSeconds(hitDuration);

        sword.transform.localRotation = Quaternion.Euler(0, 90, 0);

        hitIssued = false;
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);

        hitCooldownOver = true;
    }

    //Shielding
    void Shielding()
    {
        //If the player is NOT in utility stance
        if (!utilityOn)
        {
            //If the player is pressing right click
            if (Input.GetMouseButton(1))
            {
                //Set the rotation / position / scale to new fixed values
                shield.transform.localRotation = Quaternion.Euler(355, 0, 0);
                shield.transform.localPosition = new Vector3(-.2f, 0, .5f);
                shield.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);

                isShielding = true;
            }

            //If the player is NOT pressing right click
            if (!Input.GetMouseButton(1))
            {
                //Set the rotation / position / scale to it's original state
                shield.transform.localRotation = Quaternion.Euler(0, 293, 0);
                shield.transform.localPosition = new Vector3(-.5f, 0, .286f);
                shield.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                isShielding = false;
            }
        }
    }

    //When the player takes damage
    public void HealthDecrease()
    {
        //if player is shielding
        if (isShielding)
        {
            //The player takes damage
            health -= healthShieldDecAmount;

            //Decrease health bar visual
            healthBar.fillAmount -= healthShieldDecAmount / 10f;
        }

        //if player is NOT shielding
        else
        {
            //The player takes damage
            health -= healthDecAmount;

            //Decrease health bar visual
            healthBar.fillAmount -= healthDecAmount / 10f;
        }
    }

    //The input to changing stances. This is only for changing variables to whatever is active
    void StanceChange()
    {
        //Utility on
        if (Input.GetKeyDown("1"))
        {
            utilityOn = true;
            attackOn = false;
            defenceOn = false;
            playerMat.color = Color.green;


        }

        //Attack on
        if (Input.GetKeyDown("2"))
        {
            attackOn = true;
            utilityOn = false;
            defenceOn = false;
            playerMat.color = Color.red;

            shield.SetActive(true);
        }

        //Defence on
        if (Input.GetKeyDown("3"))
        {
            defenceOn = true;
            utilityOn = false;
            attackOn = false;
            playerMat.color = Color.blue;

            shield.SetActive(true);
        }
    }

    //Everything that changes on the player when in particular stances
    void StanceStats()
    {
        //If Utility stance is active
        if (utilityOn)
        {
            //The player's current speed is set to variable 'utilitySpeed'
            speed = utilitySpeed;

            shield.SetActive(false);
        }

        //If Utility stance is not active
        if (!utilityOn)
        {
            speed = baseSpeed;
        }

        //If Defence stance is active
        if (defenceOn)
        {
            shield.SetActive(true);
        }

        //if attack stance is active
        if (attackOn)
        {
            shield.SetActive(false);
        }
    }

    //Player movement
    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    //Player jump
    void PlayerJump()
    {
        if (Input.GetKeyDown("space") && !jumping)
        {
            //Set jumping to true
            jumping = true;

            rb.AddForce(jump * jumpStrength, ForceMode.Impulse);
            Invoke("PlayerJumpDown", jumpDownWait);
        }
    }

    void PlayerJumpDown()
    {
        Physics.gravity = new Vector3(0, gravityStr, 0); ;
        Invoke("ResetJump", .5f);
    }

    void ResetJump()
    {
        Physics.gravity = new Vector3(0, gravityNor, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            //Set jumping to false if player collides with the floor
            jumping = false;
        }
    }
}
