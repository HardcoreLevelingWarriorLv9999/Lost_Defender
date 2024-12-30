using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public string playableSceneName = "PlayableScene";

    void Start()
    {
        cutsceneDirector.Play();
    }

    void Update()
    {
        if (cutsceneDirector.state != PlayState.Playing)
        {
            // Cutscene has ended, load the playable scene
            LoadPlayableScene();
        }
    }

    void LoadPlayableScene()
    {
        SceneManager.LoadScene(playableSceneName);
    }
}
