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

    public float controllerSensitivity = 50.0f;
    public float movementSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.1f;
    public float jumpHeight = 3f;
    public float hookTravelSpeed = 15.0f;
    public float playerHookSpeed = 15.0f;
    public float maxHookDistance;

    float currentHookDistance;
    float xRotation = 0f;

    bool isGrounded;
    bool hasJumped;
	bool hasHooked;

    public bool interact;
    public bool hooked;

    public static bool hookFired;

    public int stateNo;

    public LayerMask groundMask;

    Vector3 velocity;
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

        controls.Gameplay.SwitchStatesUp.performed += context => SwitchStateUp();
        controls.Gameplay.SwitchStatesDown.performed += context => SwitchStateDown();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        currentStanceState = StanceState.ATTACK;
    }

    private void Update()
    {
        //State Switching

        if (stateNo == 4)
        {
            stateNo = 1;
        }
        else if (stateNo == 0)
        {
            stateNo = 3;
        }

        if (stateNo == 1)
        {
            currentStanceState = StanceState.ATTACK;
        }
        else if (stateNo == 2)
        {
            currentStanceState = StanceState.DEFENCE;
        }
        else if (stateNo == 3)
        {
            currentStanceState = StanceState.UTILITY;
        }

        switch (currentStanceState)
        {
            case (StanceState.ATTACK):
                //stats
                movementSpeed = 5;
                break;

            case (StanceState.DEFENCE):
                //more stats
                movementSpeed = 5;
                break;

            case (StanceState.UTILITY):
                //even more stats
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

        Vector3 move = cameraFocusX.transform.right * controllerInputLeftStick.x + cameraFocusX.transform.forward * controllerInputLeftStick.y;

        controller.Move(move * movementSpeed * Time.deltaTime);

        //------------------------------------------------------------------------------------------------------------------------------------

        //Model Rotation

        if (lastPosition != gameObject.transform.position)
        {
            if (model.transform.rotation != cameraFocusX.transform.rotation)
            {
                model.transform.rotation = cameraFocusX.transform.rotation;
            }
            //animation
        }

        lastPosition = gameObject.transform.position;

        //------------------------------------------------------------------------------------------------------------------------------------

        //Jump Function and Gravity

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (hasJumped && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        if (hooked == false)
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        //Hook Function

        if (hasHooked && hookFired == false)
		{
            hookFired = true;
		}

		if (hookFired && hooked == false)
		{
            hookHolder.transform.parent = null;
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentHookDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentHookDistance >= maxHookDistance)
			{
                ReturnHook();
			}
		}

        if (hooked)
        {
            hook.transform.parent = hookedObject.transform;

            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerHookSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            if (distanceToHook < 2)
            {
                ReturnHook();
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
        }
    }

    void ReturnHook()
	{
        hook.transform.rotation = cameraFocusY.transform.rotation;
        hook.transform.position = hookHolder.transform.position;

        hookHolder.transform.position = cameraFocusY.transform.position;
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