using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    CharController playerHealth;
    GameObject player;

    public Image fillImage;
    private Slider slider;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<CharController>();

        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }


        float fillValue = (playerHealth.currentHealth / playerHealth.maxHealth) * 100;

        slider.value = fillValue;
    }
}
