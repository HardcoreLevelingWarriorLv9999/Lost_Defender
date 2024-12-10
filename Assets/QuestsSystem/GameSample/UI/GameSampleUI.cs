using UnityEngine.UIElements;

namespace QuestGameSample
{
    /// <summary>
    /// UI quản lý các màn hình trong game
    /// </summary>
    public class GameSampleUI : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<GameSampleUI, UxmlTraits> { }

        private static GameSampleUI instance;

        public static GameSampleUI Instance
        {
            get
            {
                if (!instance.initialised)
                {
                    instance.InitComponents(); // Đảm bảo khởi tạo các UI elements
                }

                return instance;
            }
            private set { instance = value; }
        }

        public bool initialised;

        // Các màn hình UI
        private VisualElement startScreen, gameScreen, endScreen;

        public GameSampleUI()
        {
            Instance = this;
        }

        /// <summary>
        /// Hiển thị màn hình bắt đầu (được bỏ qua và chuyển trực tiếp vào game)
        /// </summary>
        public void StartScreen()
        {
            if (!initialised)
            {
                InitComponents(); // Đảm bảo UI được khởi tạo
            }

            StartGame(); // Bỏ qua start screen, gọi thẳng vào game
        }

        /// <summary>
        /// Khởi tạo các thành phần UI
        /// </summary>
        private void InitComponents()
        {
            startScreen = contentContainer.Q<VisualElement>("Start-Screen");
            gameScreen = contentContainer.Q<VisualElement>("Game-Screen");
            endScreen = contentContainer.Q<VisualElement>("End-Screen");

            if (startScreen != null)
            {
                startScreen.Q<Button>("Play-Button").clicked += StartGame;
            }

            initialised = true;
        }

        /// <summary>
        /// Tắt tất cả các màn hình
        /// </summary>
        private void DisableAllScreens()
        {
            if (startScreen != null) startScreen.style.display = DisplayStyle.None;
            if (gameScreen != null) gameScreen.style.display = DisplayStyle.None;
            if (endScreen != null) endScreen.style.display = DisplayStyle.None;
        }

        /// <summary>
        /// Bắt đầu game, hiển thị màn hình game
        /// </summary>
        public void StartGame()
        {
            if (!initialised)
            {
                InitComponents();
            }

            DisableAllScreens();
            if (gameScreen != null) gameScreen.style.display = DisplayStyle.Flex;

            GameSample.Instance.StartGame();
        }

        /// <summary>
        /// Hiển thị màn hình kết thúc (bỏ qua)
        /// </summary>
        public void EndGame()
        {
            DisableAllScreens();
        }
    }
}
