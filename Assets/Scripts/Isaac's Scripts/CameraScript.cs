using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    ControllerInput controls;

    public GameObject MainCamera;

    Vector3 rotate;

    void Awake()
    {
        controls = new ControllerInput();

        controls.Gameplay.Camera.performed += context => rotate = context.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += context => rotate = Vector2.zero;
    }

    void Update()
    {
        MainCamera.transform.LookAt(transform);

        Vector3 r = new Vector3(-rotate.x, rotate.y, 0) * 10 * Time.deltaTime;
        MainCamera.transform.Translate(r);

        Debug.Log(rotate.y);
        Debug.Log(rotate.x);


        /*float speed = 1.0f;
        transform.Rotate(Vector3.up, Input.GetAxis("Joystick Hor Right") * speed);
        //transform.Rotate(Vector3.left, Input.GetAxis("Joystick Ver Right") * speed);*/
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