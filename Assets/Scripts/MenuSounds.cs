using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSounds : MonoBehaviour
{
    public AudioSource menuSFX;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    public GameObject pauseFirst;

    public void HoverSound()
    {
        
        menuSFX.PlayOneShot(hoverSFX);


    }

    public void ClickSound()
    {
        menuSFX.PlayOneShot(clickSFX);
    }

    public void MenuFirst()
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //Set a new selected object
        EventSystem.current.SetSelectedGameObject(pauseFirst);
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
