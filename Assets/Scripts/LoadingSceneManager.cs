using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance;
    public GameObject loadingScreenObj;
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public Sprite[] backgrounds;
    public Image backgroundImg;
    public TextMeshProUGUI tipText;
    public CanvasGroup alphaGroup;
    public string[] tips;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SwitchToScene(string sceneName)
    {
        loadingScreenObj.SetActive(true);
        backgroundImg.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        StartCoroutine(GenerateTip());
        progressBar.value = 0;
        progressText.text = "Loading... 0%";
        StartCoroutine(SwitchToSceneAsync(sceneName));
    }

    IEnumerator SwitchToSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            float progressValue = asyncLoad.progress * 100;
            progressBar.value = asyncLoad.progress;
            progressText.text = "Loading... " + progressValue.ToString("F0") + "%";
            yield return null;
        }
        progressBar.value = 0.99f;
        progressText.text = "Loading... 99%";
        yield return new WaitForSeconds(1.0f);
        loadingScreenObj.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    public int tipCounter = 0;
    public IEnumerator GenerateTip()
    {
        tipCounter = Random.Range(0, tips.Length);
        tipText.text = tips[tipCounter];

        while (loadingScreenObj.activeSelf)
        {
            // Fade in
            yield return StartCoroutine(FadeInAlpha());

            // Display tip for 3 seconds
            yield return new WaitForSeconds(5f);

            // Fade out
            yield return StartCoroutine(FadeOutAlpha());

            // Change tip
            tipCounter++;
            if (tipCounter >= tips.Length)
            {
                tipCounter = 0;
            }
            tipText.text = tips[tipCounter];
        }
    }

    // Coroutine to fade in alpha
    IEnumerator FadeInAlpha()
    {
        float duration = 1f; // Duration of the fade in seconds
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            alphaGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
        alphaGroup.alpha = 1f;
    }

    // Coroutine to fade out alpha
    IEnumerator FadeOutAlpha()
    {
        float duration = 1f; // Duration of the fade in seconds
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            alphaGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / duration));
            yield return null;
        }
        alphaGroup.alpha = 0f;
    }

}
