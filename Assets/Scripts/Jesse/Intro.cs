using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Animator booBoo;
   
        
    void Start()
    {
        StartCoroutine(Splash());
    }

    public IEnumerator Splash()
    {
        yield return new WaitForSeconds(5);

        booBoo.SetTrigger("Fade Out");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
    }
}
