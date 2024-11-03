using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class Gameplay : MonoBehaviour
{
    public GameObject easy;
    public GameObject normal;
    public GameObject hard;
    public GameObject nightmare;
    public GameObject gameplay;
    public GameObject setting;
    public GameObject exit;
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
    // Start is called before the first frame update
    void Start()
    {
        
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


        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);


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
        easy.SetActive(true);
        gamemode1.SetActive(true);
    }
    public void Normmal()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        normal.SetActive(true);
        gamemode2.SetActive(true);
    }
    public void Hard()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        hard.SetActive(true);
        gamemode3.SetActive(true);
    }
    public void Nightmare()
    {
        gamemode.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        gameplay.SetActive(false);
        nightmare.SetActive(true);
        gamemode4.SetActive(true);
    }
   
    public void Exiteasy()
    {
        gamemode.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);
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
}
