using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Gameplay : MonoBehaviour
{
    public GameObject easy;
    public GameObject normal;
    public GameObject hard;
    public GameObject nightmare;
    public GameObject gameplay;
    public GameObject gameroom;
    public GameObject setting;
    public GameObject exit;
    public GameObject paneExit;
    public GameObject paneSetting;
    public GameObject gamemode;
    public GameObject gamemode1;
    public GameObject gamemode2;
    public GameObject gamemode3;
    public GameObject gamemode4;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public RectTransform imageA; // Transform của hình A
    public TextMeshProUGUI nameCharacter, lv, money, nucleus,gem;
   
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerData();
        easy.SetActive(false);
        normal.SetActive(false);
        hard.SetActive(false);
        nightmare.SetActive(false);
        gamemode1.SetActive(false);
        gamemode2.SetActive(false);
        gamemode3.SetActive(false);
        gamemode4.SetActive(false);
        gamemode.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
        paneExit.SetActive(false);
        paneSetting.SetActive(false);

        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        gameroom.SetActive(true);

    }
    // Update is called once per frame
    public void PlayScreen()
    {
        bool isActive = gamemode.activeSelf;
        gamemode.SetActive(!isActive);

        if (isActive)
        {

            imageA.rotation = Quaternion.Euler(0, 0, 0); // Trở về góc Z là 0
        }
        else
        {

            imageA.rotation = Quaternion.Euler(0, 0, -90); // Xoay góc Z là -90 độ
        }

    }
    public void Easy()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        gameroom.SetActive(false);
        easy.SetActive(true);
        gamemode1.SetActive(true);
    }
    public void Normmal()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        gameroom.SetActive(false);
        normal.SetActive(true);
        gamemode2.SetActive(true);
    }
    public void Hard()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        gameroom.SetActive(false);
        hard.SetActive(true);
        gamemode3.SetActive(true);
    }
    public void Nightmare()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        gameroom.SetActive(false);
        nightmare.SetActive(true);
        gamemode4.SetActive(true);
    }
    public void Exiteasy()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        gameroom.SetActive(true);
        easy.SetActive(false);
        gamemode1.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void ExitNormmal()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        gameroom.SetActive(true);
        normal.SetActive(false);
        gamemode2.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void ExitHard()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        gameroom.SetActive(true);
        hard.SetActive(false);
        gamemode3.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void ExitNightmare()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        gameroom.SetActive(true);
        nightmare.SetActive(false);
        gamemode4.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map1()
    {
        map1.SetActive(true);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map2()
    {
        map1.SetActive(false);
        map2.SetActive(true);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map3()
    {
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(true);
        map4.SetActive(false);
    }
    public void Map4()
    {
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(true);
    }
    public void PaneExit()
    {
        paneExit.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Back()
    {
        paneExit.SetActive(false);
        Time.timeScale = 1;
    }
    public void PlayRoom()
    {
        SceneManager.LoadScene("Scene character");
    }
    public void Setting()
    {
        bool isActive = paneSetting.activeSelf;
        paneSetting.SetActive(!isActive);
        ;
        if (!isActive)
        {
            gamemode.SetActive(false);
            exit.SetActive(false);
            gameplay.SetActive(false);
            gameroom.SetActive(false);
            imageA.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            exit.SetActive(true);
            gameplay.SetActive(true);
            gameroom.SetActive(true);
            imageA.rotation = Quaternion.Euler(0, 0, 0);
        }
        setting.SetActive(true);
    }
    public void PlayMap1()
    {
        SceneManager.LoadScene("Map 1");
    }
    void LoadPlayerData() 
    { 
        SaveLoadManager.PlayerData data = SaveLoadManager.LoadData();
        if (data != null) 
        {
            int level = data.level;
            string playerName = data.playerName;
            int Money = data.money;
            int energystone = data.energystone;
            int Gem= data.gem;
            int openMap1= data.openMap1;
            int openMap2= data.openMap2;
            int openMap3= data.openMap3;
            int openMap4= data.openMap4;
            nameCharacter.text = playerName;
            lv.text= level.ToString();
            money.text = Money.ToString();
            nucleus.text= energystone.ToString();
            gem.text= Gem.ToString();
            Debug.Log("Dữ liệu người chơi đã được tải!");
            Debug.Log("Tên người chơi: " + data.playerName); 
            Debug.Log("Level: " + data.level);
        } 
        else
        {
            Debug.LogError("Không thể tải dữ liệu người chơi!");
        }
    }
   
}