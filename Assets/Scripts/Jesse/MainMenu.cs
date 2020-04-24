using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator mainMenu;
    public Animator sideHo;
    public GameObject blocker;
    public void Start()
    {
        StartCoroutine(GamerIntro());
    }

    public IEnumerator GamerIntro()
    {
        mainMenu.SetTrigger("Begin");

        sideHo.SetTrigger("Begin");

        blocker.SetActive(false);
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

        blocker.SetActive(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(2);
    }

    public IEnumerator ByeByeStinky ()
    {
        mainMenu.SetTrigger("Choice Made");

        yield return new WaitForSeconds(2);

        mainMenu.SetTrigger("Option Gone");

        sideHo.SetTrigger("Conclude");

        blocker.SetActive(true);

        yield return new WaitForSeconds(3);

        Application.Quit();
    }

    public void QuitTime()
    {
        StartCoroutine(ByeByeStinky());
    }

}
