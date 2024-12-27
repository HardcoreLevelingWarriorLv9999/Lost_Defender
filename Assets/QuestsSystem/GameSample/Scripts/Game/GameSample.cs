using JUTPS.ItemSystem;
using QuestsSystem;
using UnityEngine;

namespace QuestGameSample
{
    public class GameSample : MonoBehaviour
    {
        public static GameSample Instance { get; private set; } // Singleton quản lý game

        public bool IsGame { get; private set; } // Trạng thái game đang chạy

        private void Awake()
        {
            Instance = this; // Khởi tạo singleton
        }

        private void Start()
        {
            // Bỏ qua start screen và vào thẳng game
            GameSampleUI.Instance.StartScreen();
        }

        /// <summary>
        /// Bắt đầu game, khởi tạo nhiệm vụ
        /// </summary>
        public void StartGame()
        {
            IsGame = true;

            if (QuestsManager.Instance != null)
            {
                Debug.LogError("QuestsManager.Instance đã được khởi tạo!");
            }
            else
            {
                Debug.LogError("QuestsManager.Instance chưa được khởi tạo!");
            }
        }

        /// <summary>
        /// Kết thúc game (không hiển thị end screen)
        /// </summary>
        public void EndGame()
        {
            IsGame = false;

            // Bỏ qua màn hình kết thúc
            GameSampleUI.Instance.EndGame();
        }
    }
}
