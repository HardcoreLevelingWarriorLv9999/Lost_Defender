using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkipCutscene : MonoBehaviour
{
    public GameObject introScreenObj;
    public string sceneName;
    public TextMeshProUGUI introText;
    public CanvasGroup alphaGroup;
    public string[] introTexts;

    void Start()
    {
        StartCoroutine(GenerateTip());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public int tipCounter = 0;
    public IEnumerator GenerateTip()
    {
        while (introScreenObj.activeSelf)
        {
            // Gán text hiện tại từ mảng introTexts
            introText.text = introTexts[tipCounter];

            // Fade in
            yield return StartCoroutine(FadeInAlpha());

            // Hiển thị text trong 3 giây
            yield return new WaitForSeconds(5f);

            // Fade out
            yield return StartCoroutine(FadeOutAlpha());

            // Chuyển sang text tiếp theo
            tipCounter++;
            if (tipCounter >= introTexts.Length)
            {
                tipCounter = 0; // Quay lại text đầu tiên nếu hết mảng
            }
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
