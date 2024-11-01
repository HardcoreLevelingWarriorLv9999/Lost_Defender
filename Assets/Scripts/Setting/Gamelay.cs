using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamelay : MonoBehaviour
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


        exit.SetActive(true);
        setting.SetActive(true);
        gameplay.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
