using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Gameplay : MonoBehaviour
{
    public GameObject easy;
    public GameObject normal;
    public GameObject hard;
    public GameObject nightmare;
    public GameObject gameplay;
   // public GameObject gameroom;
    public GameObject setting;
    public GameObject exit;
    public GameObject paneExit;
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
    public Button Map1Button, Map2Button, Map3Button, Map4Button;
    public GameObject gameSetting, Panelvolume, PanelControls, PanelGraphics, credits, Panelshortcut;
    int openMap1, openMap2,openMap3,openMap4, difficulty;
    public Texture2D customCursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        AudioManager.Instance.StopMusic("startMap1");
        AudioManager.Instance.PlayMusic("Nen");
        Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        Panelvolume.SetActive(false);
        gameSetting.SetActive(false);
        PanelControls.SetActive(false);
        PanelGraphics.SetActive(false);
        Panelshortcut.SetActive(false);

        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        credits.SetActive(true);
       // gameroom.SetActive(true);

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
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void Easy()
    {
        
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        credits.SetActive(false);
       // gameroom.SetActive(false);
        easy.SetActive(true);
        gamemode1.SetActive(true);
        difficulty = 0;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void Normmal()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        credits.SetActive(false);
      //  gameroom.SetActive(false);
        normal.SetActive(true);
        gamemode2.SetActive(true);
        difficulty = 1;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void Hard()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        credits.SetActive(false);
       // gameroom.SetActive(false);
        hard.SetActive(true);
        gamemode3.SetActive(true);
        difficulty = 2;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void Nightmare()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        credits.SetActive(false);
   //     gameroom.SetActive(false);
        nightmare.SetActive(true);
        gamemode4.SetActive(true);
        difficulty = 3;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void Exiteasy()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        credits.SetActive(true);
     //   gameroom.SetActive(true);
        easy.SetActive(false);
        gamemode1.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void ExitNormmal()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        credits.SetActive(true);
       // gameroom.SetActive(true);
        normal.SetActive(false);
        gamemode2.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void ExitHard()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        credits.SetActive(true);
      //  gameroom.SetActive(true);
        hard.SetActive(false);
        gamemode3.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
        AudioManager.Instance.PlaySFX("ClickButton");
    }
    public void ExitNightmare()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
        credits.SetActive(true);
       // gameroom.SetActive(true);
        nightmare.SetActive(false);
        gamemode4.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map1()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        map1.SetActive(true);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map2()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        map1.SetActive(false);
        map2.SetActive(true);
        map3.SetActive(false);
        map4.SetActive(false);
    }
    public void Map3()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(true);
        map4.SetActive(false);
    }
    public void Map4()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(true);
    }
    public void PaneExit()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        paneExit.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Application.Quit();
    }
    public void Back()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        paneExit.SetActive(false);
        Time.timeScale = 1;
    }
    public void PlayRoom()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        SceneManager.LoadScene("Scene character");
    }
    public void Setting()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        bool isActive = gameSetting.activeSelf;
        gameSetting.SetActive(!isActive);
        ;
        if (!isActive)
        {
            gamemode.SetActive(false);
            exit.SetActive(false);
            gameplay.SetActive(false);
            credits.SetActive(false);
          //  gameroom.SetActive(false);
            imageA.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            exit.SetActive(true);
            gameplay.SetActive(true);
            credits.SetActive(true);
            //  gameroom.SetActive(true);
            Panelvolume.SetActive(false);
            PanelControls.SetActive(false);
            PanelGraphics.SetActive(false);
            Panelshortcut.SetActive(false);
            imageA.rotation = Quaternion.Euler(0, 0, 0);
        }
        setting.SetActive(true);
    }
    public void PlayMap1()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        LoadingSceneManager.Instance.SwitchToScene("Cutscene Map 1");
        // SceneManager.LoadScene("Map 1");
    }
    public void PlayMap2()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        LoadingSceneManager.Instance.SwitchToScene("Cutscene Map 2");
        // SceneManager.LoadScene("Map 1");
    }
    public void PlayMap3()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        LoadingSceneManager.Instance.SwitchToScene("Map 3");
        // SceneManager.LoadScene("Map 1");
    }
    public void PlayMap4()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        LoadingSceneManager.Instance.SwitchToScene("Map 4");
        // SceneManager.LoadScene("Map 1");
    }
    public void LoadCredits()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        SceneManager.LoadScene("Credits");
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
            openMap1= data.openMap1;
            openMap2= data.openMap2;
            openMap3 = data.openMap3;
            openMap4 = data.openMap4;
            nameCharacter.text = playerName;
            lv.text="Lv" + level.ToString();
            money.text = Money.ToString();
            nucleus.text= energystone.ToString();
            gem.text= Gem.ToString();

        } 
        else
        {
            Debug.LogError("Không thể tải dữ liệu người chơi!");
        }
    }
    void Checkmapprogress()
    {
        if (difficulty==0 && openMap1>=0|| difficulty == 1 && openMap1 >= 1||
            difficulty == 2 && openMap1 >= 2|| difficulty == 3 && openMap1 >= 3)
        {
            Map1Button.interactable = true;
        }
        else
        {
            Map1Button.interactable = false;
        }
        if (difficulty == 0 && openMap1 >= 1 || difficulty == 1 && openMap2 >= 1 ||
           difficulty == 2 && openMap2 >= 2 || difficulty == 3 && openMap2 >= 3)
        {
            Map2Button.interactable = true;
        }
        else
        {
            Map2Button.interactable = false;
        }
        if (difficulty == 0 && openMap2 >= 1 || difficulty == 1 && openMap3 >= 1 ||
           difficulty == 2 && openMap3 >= 2 || difficulty == 3 && openMap3 >= 3)
        {
            Map3Button.interactable = true;
        }
        else
        {
            Map3Button.interactable = false;
        }
        if (difficulty == 0 && openMap3 >= 1 || difficulty == 1 && openMap4 >= 1 ||
           difficulty == 2 && openMap4 >= 2 || difficulty == 3 && openMap4 >= 3)
        {
            Map4Button.interactable = true;
        }
        else
        {
            Map4Button.interactable = false;
        }
    }
    private void Update()
    {
        Checkmapprogress();
    }
    public void Controls()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Panelvolume.SetActive(false);
        PanelControls.SetActive(true);
        PanelGraphics.SetActive(false);
        Panelshortcut.SetActive(false);
    }
    public void Graphics()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Panelvolume.SetActive(false);
        PanelControls.SetActive(false);
        PanelGraphics.SetActive(true);
        Panelshortcut.SetActive(false);
    }
    public void Volume()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Panelvolume.SetActive(true);
        PanelControls.SetActive(false);
        PanelGraphics.SetActive(false);
        Panelshortcut.SetActive(false);
    }
    public void Shortcut()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Panelvolume.SetActive(false);
        PanelControls.SetActive(false);
        PanelGraphics.SetActive(false);
        Panelshortcut.SetActive(true);
    }

}