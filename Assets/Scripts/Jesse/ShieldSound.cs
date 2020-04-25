using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSound : MonoBehaviour
{
    JesseAudioManager jesseAudioManager;
    // Start is called before the first frame update
    void Start()
    {
        jesseAudioManager = FindObjectOfType<JesseAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        jesseAudioManager.PlaySound("Block");
    }
    
}
