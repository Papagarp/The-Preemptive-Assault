﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour
{
    public GameObject staff;

    public GameObject player;

    public float magicBoltFlyingTime = 5.0f;

    public bool fired = false;

    private void Update()
    {
        if (fired)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 20.0f);

            magicBoltFlyingTime -= Time.deltaTime;

            if (magicBoltFlyingTime < 0)
            {
                fired = false;
                ReturnBolt();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        fired = false;
        ReturnBolt();
    }

    void ReturnBolt()
    {
        gameObject.transform.position = staff.transform.position;
        player.GetComponent<CharController>().reloadTime = 3.0f;
        magicBoltFlyingTime = 5.0f;
    }
}
