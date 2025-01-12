using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameIntro : MonoBehaviour
{
    public Animator startGameIntroAnimator;

    public void OnAnimationEvent()
    {
        startGameIntroAnimator.SetBool("Active", true);
    }

    public void OnAnimationEvent2()
    {
        startGameIntroAnimator.SetBool("Active", false);
    }
}
