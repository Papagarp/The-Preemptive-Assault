using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public enum BossState
    {
        IDLE,
        MOVING,
        LIGHTATTACK,
        HEAVYATTACK,
        DEATH
    }

    public BossState currentState;

    void Start()
    {
        currentState = BossState.IDLE;
    }

    void Update()
    {
        Debug.Log("Boss State: " + currentState);

        switch (currentState)
        {
            case (BossState.IDLE):
                break;

            case (BossState.MOVING):
                break;

            case (BossState.LIGHTATTACK):
                break;

            case (BossState.HEAVYATTACK):
                break;

            case (BossState.DEATH):
                break;
        }
    }
}
