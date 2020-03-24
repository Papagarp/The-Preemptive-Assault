using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioSource menuSFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    public void HoverSound()
    {
        menuSFX.PlayOneShot(hoverSFX);
    }

    public void ClickSound()
    {
        menuSFX.PlayOneShot(clickSFX);
    }
}
