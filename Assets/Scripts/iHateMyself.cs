using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iHateMyself : MonoBehaviour
{
    GameObject player;

    public GameObject spaceBarLocation;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position = spaceBarLocation.transform.position;
            player.transform.rotation = spaceBarLocation.transform.rotation;
        }
    }
}
