using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    ControllerInput controls;

    public GameObject theCamera;

    public Rigidbody rb;

    public enum StanceState
    {
        ATTACK,
        DEFENCE,
        UTILITY
    }

    public StanceState currentStanceState;

    public float movementSpeed = 10.0f;
    public float rotationSpeed = 100.0f;

    public int stateNo;

    public bool interact = false;

    Vector2 controllerInput;

    private Material playerMat;

    void Awake()
    {
        controls = new ControllerInput();

        controls.Gameplay.SwitchStatesUp.performed += context => SwitchStateUp();
        controls.Gameplay.SwitchStatesDown.performed += context => SwitchStateDown();

        controls.Gameplay.Move.performed += context => controllerInput = context.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += context => controllerInput = Vector2.zero;

        controls.Gameplay.Interact.performed += context => interact = true;
        controls.Gameplay.Interact.canceled += context => interact = false;
    }

    void Start()
    {
        currentStanceState = StanceState.ATTACK;

        playerMat = GetComponent<Renderer>().material;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if the player is holding the interact key and within the collider then the box moves according to the joystick

        switch (currentStanceState)
        {
            case (StanceState.ATTACK):
                //playerMat.color = Color.red;
                movementSpeed = 5;
                //attack dmg = 10
                //defence stat = 5
                break;

            case (StanceState.DEFENCE):
                //playerMat.color = Color.blue;
                movementSpeed = 5;
                //attack dmg = 5
                //defence stat = 10
                break;

            case (StanceState.UTILITY):
                //playerMat.color = Color.green;
                movementSpeed = 10;
                //attack dmg = 5
                //defence stat = 5
                break;
        }

        if(stateNo == 4)
        {
            stateNo = 1;
        }
        else if(stateNo == 0)
        {
            stateNo = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || stateNo == 1)
        {
            currentStanceState = StanceState.ATTACK;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || stateNo == 2)
        {
            currentStanceState = StanceState.DEFENCE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || stateNo == 3)
        {
            currentStanceState = StanceState.UTILITY;
        }

        //Testing Version
        Vector3 m = new Vector3(controllerInput.x, 0, controllerInput.y) * movementSpeed * Time.deltaTime;
        transform.Translate(m);


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
