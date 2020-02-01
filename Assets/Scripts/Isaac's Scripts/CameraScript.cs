using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    ControllerInput controls;

    public GameObject MainCamera;

    public GameObject player;

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

        Vector3 x = new Vector3(0, rotate.x, 0) * 50 * Time.deltaTime;
        Vector3 y = new Vector3(rotate.y, 0, 0) * 50 * Time.deltaTime;
        player.transform.Rotate(x);
        player.transform.Rotate(y);


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