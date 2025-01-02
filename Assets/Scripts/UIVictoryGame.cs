using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIVictoryGame : MonoBehaviour
{
    public int sceneIndex = 1; // Chỉ số Scene bạn muốn chuyển đến

    // Hàm này sẽ được gọi khi Animation Event được kích hoạt
    public void OnAnimationEvent()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
