using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    public GameObject panlbackground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       int background = PlayerPrefs.GetInt("background");
        if(background == 0 )
        {
            panlbackground.SetActive(false);
        }
        else if( background == 1 ) 
        {
            panlbackground.SetActive(true);
        }
    }
}
