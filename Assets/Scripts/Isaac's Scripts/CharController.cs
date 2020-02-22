using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    ControllerInput controls;

    CharacterController controller;

    public enum StanceState
    {
        ATTACK,
        DEFENCE,
        UTILITY
    }

    public StanceState currentStanceState;

    public GameObject mainCamera;
    public GameObject hook;
    public GameObject hookHolder;
    public GameObject hookedObject;

    public Transform model;
    public Transform cameraFocusX;
    public Transform cameraFocusY;
    public Transform groundCheck;
    public Transform grabCheck;

    public float controllerSensitivity = 50.0f;
    public float movementSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.1f;
    public float grabDistance = 0.1f;
    public float grabTimer = 1.0f;
    public float jumpHeight = 3f;
    public float hookTravelSpeed = 15.0f;
    public float playerHookSpeed = 15.0f;
    public float maxHookDistance;

    float currentHookDistance;
    float xRotation = 0f;

    bool isGrounded;
    
    bool hasJumped;
	bool hasHooked;
    bool isAimming;

    public bool interact;
    public bool canGrab;
    public bool holding;
    public bool hooked;

    public static bool hookFired;

    public int stateNo;

    public LayerMask groundMask;
    public LayerMask grabbableMask;

    Quaternion lastRotation;

    Vector3 velocity;
    Vector3 movement;
    Vector3 jumpMovement;
    Vector3 lastPosition;
    
    Vector2 controllerInputLeftStick;
    Vector3 controllerInputRightStick;
    
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

		controls.Gameplay.Hook.performed += context => hasHooked = true;
        controls.Gameplay.Hook.canceled += context => hasHooked = false;

        controls.Gameplay.Aim.performed += context => isAimming = true;
        controls.Gameplay.Aim.canceled += context => isAimming = false;

        controls.Gameplay.SwitchStatesUp.performed += context => SwitchStateUp();
        controls.Gameplay.SwitchStatesDown.performed += context => SwitchStateDown();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        stateNo = 1;
    }

    private void Update()
    {
        //State Switching

        //Debug.Log(currentStanceState);

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

        switch (currentStanceState)
        {
            case (StanceState.ATTACK):
                //stats
                hookHolder.SetActive(false);
                movementSpeed = 5;
                break;

            case (StanceState.DEFENCE):
                //more stats
                hookHolder.SetActive(false);
                movementSpeed = 5;
                break;

            case (StanceState.UTILITY):
                //even more stats
                hookHolder.SetActive(true);
                movementSpeed = 10;
                break;
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Joystick Controls

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

        //------------------------------------------------------------------------------------------------------------------------------------

        //Model Rotation

        if (lastPosition != gameObject.transform.position && !hooked && !holding)
        {
            if (!isGrounded)
            {
                lastRotation = model.transform.rotation;
            }
            
            if (isGrounded)
            {
                if (movement == Vector3.zero)
                {
                    model.transform.rotation = lastRotation;
                }
                model.transform.rotation = Quaternion.LookRotation(movement);
            }
            //right here
        }

        lastPosition = gameObject.transform.position;

        //------------------------------------------------------------------------------------------------------------------------------------

        //Jump Function and Gravity

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (hasJumped && isGrounded && !holding)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
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
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Hook Function

        if (currentStanceState == StanceState.UTILITY && !holding)
        {
            if (hasHooked && !hookFired)
            {
                hookFired = true;
            }

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
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Push and Pull Function

        canGrab = Physics.CheckSphere(grabCheck.position, grabDistance, grabbableMask);

        grabTimer -= Time.deltaTime;

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

        //------------------------------------------------------------------------------------------------------------------------------------
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
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