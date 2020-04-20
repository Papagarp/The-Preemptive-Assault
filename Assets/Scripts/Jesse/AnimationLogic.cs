using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    JesseAudioManager jesseAudioManager;
    void start()
    {
        jesseAudioManager = FindObjectOfType<JesseAudioManager>();
        
    }

    public void Step()
    {
        jesseAudioManager.PlaySound("Stepping");
    }

    public void SwordSwing()
    {
        
    }

    public void block()
    {

    }

    public void playerhit()
    {

    }

}
