using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerSceneCharacter : MonoBehaviour
{
    public Image displayImage; // Hình ảnh UI để hiển thị
    public Sprite[] images; // Mảng chứa 9 tấm hình
    public string nextSceneName;
    void Start()
    {
        StartCoroutine(ShowImages());
    }

    IEnumerator ShowImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            displayImage.sprite = images[i]; // Hiển thị hình ảnh từ mảng
            yield return new WaitForSeconds(1); // Đợi 1 giây
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
