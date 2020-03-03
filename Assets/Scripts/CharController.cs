using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{

    //z locking
    //health


    #region Variables

    MagicBolt magicBoltScript;
    ControllerInput controls;
    CharacterController controller;

    //Animator characterAnimator;

    public enum StanceState
    {
        ATTACK,
        DEFENCE,
        UTILITY
    }

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

    [Header("Player Movement")]
    public float movementSpeed;
    public float controllerSensitivity = 50.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    float groundDistance = 0.1f;
    float xRotation = 0f;
    bool hasJumped;
    bool isAimming;
    bool isGrounded;
    public int stateNo;
    Vector2 controllerInputLeftStick;
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
    public GameObject stunRangeObject;

    [Header("Don't Assign")]
    public GameObject hookedObject;

    #endregion

    private void Awake()
    {
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

        controls.Gameplay.SwitchStatesUp.performed += context => SwitchStateUp();
        controls.Gameplay.SwitchStatesDown.performed += context => SwitchStateDown();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        //characterAnimator = model.GetComponent<Animator>();

        magicBoltScript = magicBolt.GetComponent<MagicBolt>();
        
        stateNo = 1;
    }

    private void Update()
    {
        #region State Switching

        #region StateNo If statements
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

        if (isGrounded)
        {
            movement = cameraFocusX.transform.right * controllerInputLeftStick.x + cameraFocusX.transform.forward * controllerInputLeftStick.y;
            controller.Move(movement * movementSpeed * Time.deltaTime);
        }

        #endregion

        #region Model Rotation

        if (lastPosition != gameObject.transform.position && !hooked && !holding)
        {
            if (!isGrounded) lastRotation = model.transform.rotation;

            if (isGrounded)
            {
                if (movement == Vector3.zero) model.transform.rotation = lastRotation;

                model.transform.rotation = Quaternion.LookRotation(movement);
            }

            //characterAnimator.SetTrigger("Walk");
            //right here
        }

        lastPosition = gameObject.transform.position;

        #endregion

        #region Jump Function and Gravity

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        if (hasJumped && isGrounded && !holding) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (!hooked)
        {
            if (!isGrounded)
            {
                jumpMovement = new Vector3(movement.x, 0f, movement.z);

                controller.Move(jumpMovement * movementSpeed * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
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
                if (!isGrounded)
                {
                    //personally i dont like this and it should be done better so i will come back to this later
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 17f);
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

        

        #endregion
    }

    void Attack()
    {
        
        if (currentStanceState == StanceState.ATTACK)
        {
            sword.GetComponent<Sword>().spinAttack = false;
            //play sword animation
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

    void Ability()
    {
        if (currentStanceState == StanceState.ATTACK)
        {
            sword.GetComponent<Sword>().spinAttack = true;
            //play spin animation
        }
        else if (currentStanceState == StanceState.DEFENCE)
        {
            //stunRangeObject.GetComponent<Shield>().StunAttack();
        }
        else if (currentStanceState == StanceState.UTILITY && !holding)
        {
            if (!hookFired)
            {
                hookFired = true;
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

    void SwitchStateUp()
    {
        stateNo++;
    }

    void SwitchStateDown()
    {
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