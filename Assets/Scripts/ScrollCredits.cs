using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour
{
    public GameObject creditsRun;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Scroll());   
    }

    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(0.5f);
        creditsRun.SetActive(true);
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(4);
    }
}
