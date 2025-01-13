using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public string playableSceneName = "PlayableScene";
    public Animator fadeAnimator; // Animator cho hiệu ứng fade in
    public float fadeInStartOffset = 0.1f; // Thời gian (giây) trước khi timeline kết thúc để bắt đầu fade in

    private bool isFading = false; // Trạng thái kiểm tra xem fade in đã bắt đầu chưa

    void Start()
    {
        cutsceneDirector.Play();
    }

    void Update()
    {
        // Kiểm tra nếu timeline gần kết thúc và fade in chưa được kích hoạt
        if (!isFading && cutsceneDirector.state == PlayState.Playing)
        {
            double timeRemaining = cutsceneDirector.duration - cutsceneDirector.time;

            if (timeRemaining <= fadeInStartOffset)
            {
                StartFadeIn();
            }
        }
        else if (cutsceneDirector.state != PlayState.Playing && isFading)
        {
            // Khi timeline kết thúc và fade in đã chạy, load scene
            LoadPlayableScene();
        }
    }

    void StartFadeIn()
    {
        isFading = true;
        fadeAnimator.SetBool("Active", true); // Kích hoạt hiệu ứng fade in
    }

    void LoadPlayableScene()
    {
        SceneManager.LoadScene(playableSceneName);
    }
}
