using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSequence : MonoBehaviour 
{
    public static int sceneIndex = 0;
    public float waitTime = 5f;
    public float countdownTime; // Thêm biến đếm ngược

    // Start is called before the first frame update
    void Start() 
    {
        if(sceneIndex == 0) 
        {
            StartCoroutine(FirstScene());
        }
        if(sceneIndex == 1) 
        {
            countdownTime = waitTime; // Khởi tạo biến đếm ngược
            StartCoroutine(SecondScene());
        }
    }

    IEnumerator FirstScene() 
    {
        yield return new WaitForSeconds(5);
        sceneIndex = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator SecondScene() 
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        sceneIndex = 2;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
