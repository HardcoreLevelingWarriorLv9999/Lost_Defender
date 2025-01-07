using System.Collections;
using System.Collections.Generic;
using JUTPS;
using JUTPS.CameraSystems;
using UnityEngine;

public class Letter : MonoBehaviour
{
    //The UI version of the letter that appears after picking up the letter.
    public GameObject letterUI;

    //The toggle bool determines if the letter is being picked up or put down.
    bool toggle;

    public JUCharacterController player;

    public JUCameraController cameraController;

    //The Mesh Renderer component of your letter that disables after picking up the letter and enables when putting it back down.
    public Renderer letterMesh;

    //Function to open and close the letter.
    public void openCloseLetter()
    {
        //Toggle will equal to the opposite of what it currently equals to.
        toggle = !toggle;

        //If toggle equals false, that means the player is putting down the letter.
        if(toggle == false)
        {
            letterUI.SetActive(false);
            letterMesh.enabled = true;
            player.enabled = true;
            cameraController.enabled = true;
        }

        //If toggle equals true, that means the player is picking up the letter.
        if (toggle == true)
        {
            letterUI.SetActive(true);
            letterMesh.enabled = false;
            player.enabled = false;
            cameraController.enabled = false;
        }
    }
}
