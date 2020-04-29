using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
         
    void Start()
    {
        StartCoroutine(Splash());
    }

    public IEnumerator Splash()
    {

        yield return new WaitForSeconds(30);

        SceneManager.LoadScene("CS_GBv3");

    }
}
