using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public enum StanceState
    {
        ATTACK,
        DEFENCE,
        UTILITY
    }

    public StanceState currentStanceState;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    private Material playerMat;

    void Start()
    {
        currentStanceState = StanceState.ATTACK;

        playerMat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        switch (currentStanceState)
        {
            case (StanceState.ATTACK):
                playerMat.color = Color.red;
                //movement speed = 5
                //attack dmg = 10
                //defence stat = 5
                break;

            case (StanceState.DEFENCE):
                playerMat.color = Color.blue;
                //movement speed = 5
                //attack dmg = 5
                //defence stat = 10
                break;

            case (StanceState.UTILITY):
                playerMat.color = Color.green;
                //movement speed = 10
                //attack dmg = 5
                //defence stat = 5
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentStanceState = StanceState.ATTACK;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentStanceState = StanceState.DEFENCE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentStanceState = StanceState.UTILITY;
        }

        float translation = Input.GetAxis("Vertical") * speed;
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        //transform.Rotate(0, rotation, 0);
    }
}
