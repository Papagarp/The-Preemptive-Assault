using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//TEST VERSION


public class CharController : MonoBehaviour
{
    ControllerInput controls;

    public GameObject MainCamera;
    public Transform cameraFocus;
    
    Vector2 controllerInputLeftStick;
    Vector3 controllerInputRightStick;
    
    

    public float controllerSensitivity = 50.0f;

    private void Awake()
    {
        controls = new ControllerInput();

        controls.Gameplay.Move.performed += context => controllerInputLeftStick = context.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += context => controllerInputLeftStick = Vector2.zero;

        controls.Gameplay.Camera.performed += context => controllerInputRightStick = context.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += context => controllerInputRightStick = Vector2.zero;
    }

    private void Update()
    {
        MainCamera.transform.LookAt(cameraFocus);

        //Vector3 rightStickX = new Vector3(0, controllerInputRightStick.x, 0) * controllerSensitivity * Time.deltaTime;
        //Vector3 rightStickY = new Vector3(controllerInputRightStick.y, 0, 0) * controllerSensitivity * Time.deltaTime;

        float rightStickX = controllerInputRightStick.x * controllerSensitivity * Time.deltaTime;
        float rightStickY = controllerInputRightStick.y * controllerSensitivity * Time.deltaTime;

        cameraFocus.transform.Rotate(rightStickX * transform.up);
        cameraFocus.transform.Rotate(rightStickY * transform.right);
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
