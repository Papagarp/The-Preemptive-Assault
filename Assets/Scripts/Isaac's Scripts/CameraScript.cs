using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject MainCamera;

    void Update()
    {
        MainCamera.transform.LookAt(transform);

        float speed = 1.0f;
        transform.Rotate(Vector3.up, Input.GetAxis("Joystick Hor Right") * speed);
        //transform.Rotate(Vector3.left, Input.GetAxis("Joystick Ver Right") * speed);
    }
}