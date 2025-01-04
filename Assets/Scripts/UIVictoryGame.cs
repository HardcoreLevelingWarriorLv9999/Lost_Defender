using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVictoryGame : MonoBehaviour
{
    public string sceneName; // Chỉ số Scene bạn muốn chuyển đến

    // Hàm này sẽ được gọi khi Animation Event được kích hoạt
    public void OnAnimationEvent()
    {
        LoadingSceneManager.Instance.SwitchToScene(sceneName);
        // SceneManager.LoadScene(sceneName);
    }
}
