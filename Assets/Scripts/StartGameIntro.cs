using System.Collections;
using System.Collections.Generic;
using JUTPS;
using JUTPS.CameraSystems;
using UnityEngine;

public class StartGameIntro : MonoBehaviour
{
    public Animator startGameIntroAnimator;
    public JUCameraController cameraController;
    public List<JUCharacterController> players;

    public void OnAnimationEvent()
    {
        startGameIntroAnimator.SetBool("Active", true);
        cameraController.enabled = false;
        players.ForEach(player => player.enabled = false);
    }

    public void OnAnimationEvent2()
    {
        startGameIntroAnimator.SetBool("Active", false);
        cameraController.enabled = true;
        players.ForEach(player => player.enabled = true);
    }
}
