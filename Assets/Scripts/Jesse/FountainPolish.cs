using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainPolish : MonoBehaviour
{
    public GameObject checkpointTxt;

    void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag ("Player"))
        {
            StartCoroutine(CheckPointText());
            Debug.Log("hghghghghgh");
        }
    }

    public IEnumerator CheckPointText()
    {
        checkpointTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        checkpointTxt.SetActive(false);
    }
}
