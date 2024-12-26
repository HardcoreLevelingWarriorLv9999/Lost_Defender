using System.Collections.Generic;
using JUTPS;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour 
{
    public List<JUHealth> healthObjects; // Danh sách đối tượng chứa script máu (người chơi và xe)
    public GameObject gameOverUI; // UI GameOver cần hiển thị

    void Start() 
    {
        // Ẩn UI GameOver khi bắt đầu game
        gameOverUI.SetActive(false);

        // Đăng ký sự kiện chết của mỗi đối tượng trong danh sách
        foreach (JUHealth healthObject in healthObjects) 
        {
            healthObject.OnDeath.AddListener(HandleGameOver);
        }
    }

    void HandleGameOver() 
    {
        // Hiển thị UI GameOver khi bất kỳ đối tượng nào chết
        gameOverUI.SetActive(true);
    }
}
