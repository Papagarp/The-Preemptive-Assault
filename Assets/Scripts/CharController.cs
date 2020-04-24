using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum StanceState
{
    ATTACK,
    DEFENCE,
    UTILITY
}

public class CharController : MonoBehaviour
{
    //TODO: health system

    #region Variables

    JesseAudioManager jesseAudioManager;
    MagicBolt magicBoltScript;
    ControllerInput controls;
    CharacterController controller;
    Animator playerAnimatorComponent;
    Coroutine swingCoroutine;
    Coroutine jumpCoroutine;

    public StanceState currentStanceState;

    [Header("Assign Transforms")]
    public Transform cameraFocusX;
    public Transform cameraFocusY;
    public Transform groundCheck;
    public Transform grabCheck;

    [Header("Assign GameObjects")]
    public GameObject mainCamera;
    public GameObject model;
    public GameObject hook;
    public GameObject hookHolder;

    [Header("Assign Masks")]
    public LayerMask groundMask;
    public LayerMask grabbableMask;

    [Header("Player")]
    public float stepTimer;
    float stepTimerCount;
    public float maxHealth;
    public float currentHealth;
    public float currentSpeed;
    public float movementSpeed;
    public float controllerSensitivity = 50.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    float groundDistance = 0.1f;
    float xRotation = 0f;
    public bool hasJumped;
    bool hasSwung;
    bool isAimming;
    public bool isGrounded;
    public int stateNo;
    public Vector2 controllerInputLeftStick;
    Vector3 controllerInputRightStick;
    Vector3 jumpMovement;
    Vector3 lastPosition;
    Vector3 velocity;
    Vector3 movement;
    Quaternion lastRotation;

    [Header("Player Stance Components")]
    public GameObject sword;
    public GameObject shield;
    public GameObject staff;
    public GameObject magicBolt;
    public bool isAttacking;
    public float reloadTime = 0.0f;
    
    [Header("Hook Function")]
    public float hookTravelSpeed = 15.0f;
    public float playerHookSpeed = 15.0f;
    public float maxHookDistance;
    float currentHookDistance;
    public bool hooked;
    public static bool hookFired;

    [Header("Interact Function")]
    public bool interact;
    public float grabDistance = 0.1f;
    public float grabTimer = 1.0f;
    public bool canGrab;
    public bool holding;

    [Header("Stun Function")]
    public bool stunned;
    public float stunnedTime = 3.0f;

    /*[Header("Lock On Function")]
    public bool locked = false;
    public GameObject[] enemyLocations;
    public GameObject closestEnemy;*/

    [Header("Don't Assign")]
    public GameObject hookedObject;

    #endregion

    private void Awake()
    {
        playerAnimatorComponent = GetComponentInChildren<Animator>();

        controls = new ControllerInput();

        controls.Gameplay.Move.performed += context => controllerInputLeftStick = context.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += context => controllerInputLeftStick = Vector2.zero;

        controls.Gameplay.Camera.performed += context => controllerInputRightStick = context.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += context => controllerInputRightStick = Vector2.zero;

        controls.Gameplay.Jump.performed += context => hasJumped = true;
        controls.Gameplay.Jump.canceled += context => hasJumped = false;

        controls.Gameplay.Interact.performed += context => interact = true;
        controls.Gameplay.Interact.canceled += context => interact = false;

        controls.Gameplay.Aim.performed += context => isAimming = true;
        controls.Gameplay.Aim.canceled += context => isAimming = false;

        controls.Gameplay.Attack.performed += context => Attack();
        controls.Gameplay.Ability.performed += context => Ability();
        //controls.Gameplay.LockOn.performed += context => LockOn();

        controls.Gameplay.SwitchStatesUp.performed += context => SwitchStateUp();
        controls.Gameplay.SwitchStatesDown.performed += context => SwitchStateDown();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        magicBoltScript = magicBolt.GetComponent<MagicBolt>();
        
        stateNo = 1;

        jesseAudioManager = FindObjectOfType<JesseAudioManager>();

        stepTimerCount = stepTimer;
        jesseAudioManager.PlaySound("Music");
    }

    private void Update()
    {
        #region State Switching

        playerAnimatorComponent.SetInteger("Current State", (int)currentStanceState);

        #region StateNo If statements
;
        //TODO: rewrite state switching to be like 10 lines (maybe use a method?)
        if (stateNo == 4)
        {
            stateNo = 1;
        }
        if (stateNo == 0)
        {
            stateNo = 3;
        }

        if (stateNo == 1)
        {
            currentStanceState = StanceState.ATTACK;
        }
        if (stateNo == 2)
        {
            currentStanceState = StanceState.DEFENCE;
        }
        if (stateNo == 3)
        {
            currentStanceState = StanceState.UTILITY;
        }

        #endregion

        switch (currentStanceState)
        {
            case (StanceState.ATTACK):

                
                hookHolder.SetActive(false);
                sword.SetActive(true);
                shield.SetActive(false);
                staff.SetActive(false);

                movementSpeed = 7.5f;

                break;

            case (StanceState.DEFENCE):

                
                hookHolder.SetActive(false);
                sword.SetActive(false);
                shield.SetActive(true);
                staff.SetActive(false);

                movementSpeed = 5f;

                break;

            case (StanceState.UTILITY):

                
                hookHolder.SetActive(true);
                sword.SetActive(false);
                shield.SetActive(false);
                staff.SetActive(true);

                movementSpeed = 10f;

                if (reloadTime >= 0) reloadTime -= Time.deltaTime;

                break;
        }

        #endregion

        #region Joystick Controls

        mainCamera.transform.LookAt(cameraFocusY);

        float rightStickX = controllerInputRightStick.x * controllerSensitivity * Time.deltaTime;
        float rightStickY = controllerInputRightStick.y * controllerSensitivity * Time.deltaTime;

        xRotation -= rightStickY;
        xRotation = Mathf.Clamp(xRotation, -25f, 25f);

        cameraFocusY.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraFocusX.Rotate(Vector3.up * rightStickX);

        if (isGrounded && !isAttacking)
        {
            movement = cameraFocusX.transform.right * controllerInputLeftStick.x + cameraFocusX.transform.forward * controllerInputLeftStick.y;
            controller.Move(movement * movementSpeed * Time.deltaTime);
        }

        #endregion

        #region Health Bar

        //ALBERT RIGHT HERE!!!

        #endregion

        #region Model Rotation and Movement Animation

        if (lastPosition != gameObject.transform.position && !hooked && !holding)
        {
            currentSpeed = (gameObject.transform.position - lastPosition).magnitude;

            if (!isGrounded) lastRotation = model.transform.rotation;

            if (isGrounded)
            {
                if (movement == Vector3.zero) model.transform.rotation = lastRotation;

                model.transform.rotation = Quaternion.LookRotation(movement);
            }
            playerAnimatorComponent.SetBool("Moving", true);

           if (stepTimerCount > 0)
           {
                stepTimerCount -= Time.deltaTime;
            }
           else {
                stepTimerCount = stepTimer;
                jesseAudioManager.PlaySound("Stepping");
           }
        }
        else
        {
            playerAnimatorComponent.SetBool("Moving", false);
            stepTimerCount = stepTimer;
        }

        lastPosition = gameObject.transform.position;

        #endregion

        #region Death

        if (currentHealth >= 0)
        {
            //Might Make a coroutine for death on this one but this works for barebone stuff
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion

        #region Jump Function and Gravity

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (hasJumped && isGrounded && !holding)
        {
            Jump();
        }
        
        if (!hooked)
        {
            if (!isGrounded)
            {
                jumpMovement = new Vector3(movement.x, 0f, movement.z);

                controller.Move(jumpMovement * movementSpeed * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if(velocity.y < 0 && !isGrounded)
            {
                //TODO: Falling animation
            }
        }

        #endregion

        #region Push and Pull Function

        canGrab = Physics.CheckSphere(grabCheck.position, grabDistance, grabbableMask);

        if (grabTimer >= 0) grabTimer -= Time.deltaTime;

        if (holding && interact && grabTimer <= 0.0f)
        {
            holding = false;
            grabTimer = 1.0f;
        }

        if (canGrab && interact && grabTimer <= 0.0f)
        {
            holding = true;
            grabTimer = 1.0f;
        }

        #endregion

        #region Hook Function

        //TODO: Finish the polish on the hook including the prefabs
        if (hookFired && !hooked)
        {
            hookHolder.transform.parent = null;
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentHookDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentHookDistance >= maxHookDistance)
            {
                ReturnHook();
            }
        }

        if (hooked && hookFired)
        {
            hook.transform.parent = hookedObject.transform;

            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerHookSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            if (distanceToHook < 2)
            {

                hook.SetActive(false);
                if (!isGrounded)
                {
                    //personally i dont like this and it should be done better so i will come back to this later
                    //this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    //this.transform.Translate(Vector3.up * Time.deltaTime * 17f);
                }

                StartCoroutine("Climb");

            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
        }

        #endregion

        #region Shield Stun

        if (stunned)
        {
            stunnedTime -= Time.deltaTime;

            if (stunnedTime <= 0)
            {
                stunned = false;
                stunnedTime = 3.0f;
            }
        }

        #endregion

        /*#region Lock On

        if (closestEnemy != null && locked)
        {
            //TODO: Lock on Ui element turn on
        }

        #endregion*/
    }

    public void TakeDmg(float damage)
    {
        currentHealth = currentHealth - damage;
        jesseAudioManager.PlaySound("Hit");
    }

    void Attack()
    {
        if (isGrounded)
        {
            if (currentStanceState == StanceState.ATTACK)
            {
                sword.GetComponent<Sword>().spinAttack = false;

                Swing();
            }
            else if (currentStanceState == StanceState.DEFENCE)
            {
                //is there ever meant to be an attack in this state?
            }
            else if (currentStanceState == StanceState.UTILITY)
            {
                if (!magicBoltScript.fired)
                {
                    if (reloadTime < 0) magicBoltScript.fired = true;
                }
            }
        }
    }

    void Ability()
    {
        if (isGrounded)
        {
            if (currentStanceState == StanceState.ATTACK)
            {
                sword.GetComponent<Sword>().spinAttack = true;
                //play spin animation
            }
            else if (currentStanceState == StanceState.DEFENCE && !stunned)
            {
                stunned = true;
            }
            else if (currentStanceState == StanceState.UTILITY && !holding)
            {
                hook.SetActive(true);
                if (!hookFired)
                {
                    hookFired = true;
                }
            }
        }
    }

    void ReturnHook()
    {
        hookHolder.transform.position = cameraFocusY.transform.position;

        hook.transform.position = hookHolder.transform.position;
        hook.transform.rotation = cameraFocusY.transform.rotation;

        hookHolder.transform.parent = cameraFocusY.transform;

        hookFired = false;
        hooked = false;
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }
    
    /*void LockOn()
    {
        //FindClosestEnemy(enemyLocations);
        locked = !locked;
    }

    public void FindClosestEnemy(GameObject[] go)
    {
        enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        for(enemyLocations)
        {
            Vector3 diff = closestEnemy.transform.position - position;
            float currentDistance = diff.sqrMagnitude;

            if (currentDistance <= distance)
            {
                closestEnemy = go;
                distance = currentDistance;
            }

        }
        return closestEnemy;
    }*/

    public void Jump()
    {
        if (jumpCoroutine == null)
        {
            jumpCoroutine = StartCoroutine(JumpAnimation());
        }
    }

    IEnumerator JumpAnimation()
    {
        playerAnimatorComponent.SetTrigger("Jumping");

        float jumpAnimationLength = -1;

        foreach (AnimationClip clip in playerAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Main_char_jump_test_001")
            {
                jumpAnimationLength = clip.length;
            }
        }

        while (jumpAnimationLength > 0)
        {
            jumpAnimationLength -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        jumpCoroutine = null;
    }

    public void Swing()
    {
        isAttacking = true;
        hasSwung = true;

        //start the attack combo
        if (swingCoroutine == null)
        {
            swingCoroutine = StartCoroutine(AttackCombo());
        }
    }

    IEnumerator AttackCombo()
    {
        playerAnimatorComponent.SetTrigger("SwordAttackPart1");
        jesseAudioManager.PlaySoundWithDelay("Sword Swing", 0.3f);
        print("part1");

        float firstAnimationLength = -1;
        float secondAnimationLength = -1;

        foreach (AnimationClip clip in playerAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "SwordAttackAnim1")
            {
                firstAnimationLength = clip.length;
            }
            if (clip.name == "SwordAttackAnim2")
            {
                secondAnimationLength = clip.length;
            }
        }

        print(firstAnimationLength);
        print(secondAnimationLength);

        //prevent a second attack
        float timer = firstAnimationLength * 0.75f;//first 75% of the animation, dont allow the second attack
        while (timer > 0)
        {
            hasSwung = false;
            playerAnimatorComponent.SetBool("SwordAttackPart2", hasSwung);
            print("part2");
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //small window for second attack check
        bool swinging = false;
        float hitWindow = firstAnimationLength * 0.25f;//25% of the end of the animation
        while (hitWindow > 0)
        {
            //if we've pressed the attack button during the window swing into the second attack
            if (hasSwung)
            {
                //wait for the second animation to end
                swinging = true;
                playerAnimatorComponent.SetBool("SwordAttackPart2", hasSwung);
                print("part3");
            }

            hitWindow -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //wait longer for the second animation if true
        if (swinging)
        {
            yield return new WaitForSeconds(secondAnimationLength);
            print("part4");
        }

        hasSwung = false;
        if (!swinging)
        {
            playerAnimatorComponent.SetBool("SwordAttackPart2", hasSwung);
            print("part5");
        }

        swingCoroutine = null;
        isAttacking = false;
    }

    void SwitchStateUp()
    {
        //currentStanceState++;
        jesseAudioManager.PlaySound("Stance Swap");
        stateNo++;
    }

    void SwitchStateDown()
    {
        //currentStanceState--;
        jesseAudioManager.PlaySound("Stance Swap");
        stateNo--;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}