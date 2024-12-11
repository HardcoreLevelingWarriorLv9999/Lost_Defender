using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerSceneCharacter : MonoBehaviour
{
    public Image displayImage; // Hình ảnh UI để hiển thị
    public Sprite[] images; // Mảng chứa 9 tấm hình
    public TextMeshProUGUI nameCharacter;
    public TextMeshProUGUI characterInformation;
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
    public GameObject Character4;
    private GameObject currentCharacter;
  
    void Start()
    {
        StartCoroutine(ShowImages());
        nameCharacter.text = "Alex Rodriguez";
        characterInformation.text = "Alex Rodriguez character information ";
        DestroyCurrentCharacter();
        CreateCharacter(Character1);
        PlayerPrefs.SetString("PlayerName", "Alex Rodriguez");
    }

    IEnumerator ShowImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            displayImage.sprite = images[i]; // Hiển thị hình ảnh từ mảng
            yield return new WaitForSeconds(1); // Đợi 1 giây
        }

    }
    public void AlexRodriguez()
    {
        nameCharacter.text = "Alex Rodriguez";
        characterInformation.text = "Alex Rodriguez character information ";
        DestroyCurrentCharacter(); 
        CreateCharacter(Character1);
        PlayerPrefs.SetString("PlayerName", "Alex Rodriguez");
    }
    public void KaitoNakamura()
    {
        nameCharacter.text = "Kaito Nakamura";
        characterInformation.text = "Kaito Nakamura character information";
        DestroyCurrentCharacter(); 
        CreateCharacter(Character2);
        PlayerPrefs.SetString("PlayerName", "Kaito Nakamura");
    }
    public void MeiChen()
    {
        nameCharacter.text = "Mei Chen";
        characterInformation.text = "Mei Chen character information";
        DestroyCurrentCharacter();
        CreateCharacter(Character3);
        PlayerPrefs.SetString("PlayerName", "Mei Chen");
    }
    public void SarahEvans()
    {
        nameCharacter.text = "Sarah Evans";
        characterInformation.text = "Sarah Evans character information";
        DestroyCurrentCharacter();
        CreateCharacter(Character4);
        PlayerPrefs.SetString("PlayerName", "Sarah Evans");
    }

    void DestroyCurrentCharacter()
    {
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }
    }
    void CreateCharacter(GameObject character) 
    {
        currentCharacter = Instantiate(character, new Vector3(0, -0.3f, -8), Quaternion.Euler(0, 180, 0));
        currentCharacter.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
}
