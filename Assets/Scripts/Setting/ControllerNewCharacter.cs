using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerNewCharacter : MonoBehaviour
{
   
    public TextMeshProUGUI nameCharacter;
    public TextMeshProUGUI characterInformation;
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
    public GameObject Character4;
    private GameObject currentCharacter;
    int characternumber;
    // Start is called before the first frame update
    void Start()
    {
        SavePlayerData();
        nameCharacter.text = "Alex Rodriguez";
        characterInformation.text = "he is the leader with a 5% increase in melee weapon damage ";
        DestroyCurrentCharacter();
        CreateCharacter(Character1);
        characternumber =1;
    }

    public void AlexRodriguez()
    {
        characternumber =1;
        nameCharacter.text = "Alex Rodriguez";
        characterInformation.text = "He is the leader with a 5% increase in melee weapon damage ";
        DestroyCurrentCharacter();
        CreateCharacter(Character1);
    }
    public void KaitoNakamura()
    {
        characternumber =2;
        nameCharacter.text = "Kaito Nakamura";
        characterInformation.text = "He is a reliable defender with the ability to reduce damage taken by 5%.";
        DestroyCurrentCharacter();
        CreateCharacter(Character2);
    }
    public void MeiChen()
    {
        characternumber =3;
        nameCharacter.text = "Mei Chen";
        characterInformation.text = "She is a technology master with the ability to increase ranged weapon damage by 5% thanks to her research.";
        DestroyCurrentCharacter();
        CreateCharacter(Character3);
    }
    public void SarahEvans()
    {
        characternumber =4;
        nameCharacter.text = "Sarah Evans";
        characterInformation.text = "She is a medic with the ability to restore 0.1% HP per second";
        DestroyCurrentCharacter();
        CreateCharacter(Character4);
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
    void SavePlayerData()
    {
        SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
        string nameGame = PlayerPrefs.GetString("FileGame");
        data.level = 1;
        data.exp = 0;
        data.money = 0;
        data.gem = 0;
        data.energystone = 0;
        data.openMap1 = 0;
        data.openMap2 = 0;
        data.openMap3 = 0;
        data.openMap4 = 0;
        data.playerName = nameGame;
        data.characternumber = characternumber;
        SaveLoadManager.SaveData(data);
        Debug.Log("Dữ liệu người chơi đã được lưu!");
        Debug.Log("Tên người chơi: " + data.playerName);
        Debug.Log("Mã nhân vật: " + data.characternumber);
        Debug.Log("Level: " + data.level);
    }
    public void PlayGame()
    {
        SavePlayerData();
        SceneManager.LoadScene("MenuScene");
    }
}
