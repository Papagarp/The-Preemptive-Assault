using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDogAnimator : MonoBehaviour
{
    Animator aiDogAnimatorComponent;
    AiDogController aiDogController;

    private void Awake()
    {
        aiDogAnimatorComponent = GetComponentInChildren<Animator>();
        aiDogController = GetComponent<AiDogController>();
    }

    private void Update()
    {
        aiDogAnimatorComponent.SetBool("Moving", aiDogController.nav.remainingDistance > 1 ? true : false);
    }
}
