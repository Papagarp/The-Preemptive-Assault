using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator mainMenu;
    public Animator sideHo;

    public void Start()
    {
        StartCoroutine(GamerIntro());
    }

    public IEnumerator GamerIntro()
    {
        yield return new WaitForSeconds(2);

        mainMenu.SetTrigger("Title Done");
    }

    public void Gamertime()
    {
        StartCoroutine(Extremo());
    }

    public IEnumerator Extremo()
    {
        mainMenu.SetTrigger("Choice Made");

        yield return new WaitForSeconds(2);

        mainMenu.SetTrigger("Option Gone");

        sideHo.SetTrigger("Conclude");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("JesseTest");
    }

}
