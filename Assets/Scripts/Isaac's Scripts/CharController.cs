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
    
    float yRotation = 0f;

    public float controllerSensitivity = 50.0f;

    private void Awake()
    {
        controls = new ControllerInput();

        controls.Gameplay.Move.performed += context => controllerInputLeftStick = context.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += context => controllerInputLeftStick = Vector2.zero;

        controls.Gameplay.Camera.performed += context => controllerInputRightStick = context.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += context => controllerInputRightStick = Vector2.zero;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        MainCamera.transform.LookAt(cameraFocus);

        float rightStickX = controllerInputRightStick.x * controllerSensitivity * Time.deltaTime;
        float rightStickY = controllerInputRightStick.y * controllerSensitivity * Time.deltaTime;

        yRotation -= rightStickY;
        yRotation = Mathf.Clamp(yRotation, -25f, 25f);

        cameraFocus.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        cameraFocus.Rotate(Vector3.right * rightStickY);

        

        //cameraFocus.transform.Rotate(rightStickX * transform.up);
        //cameraFocus.transform.Rotate(rightStickY * transform.right);
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
