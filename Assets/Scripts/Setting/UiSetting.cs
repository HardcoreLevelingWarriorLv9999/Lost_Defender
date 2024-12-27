using JUTPS.GameSettings;
using JUTPS.InputEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JUTPS.UI
{
    /// <summary>
    /// The game settings screen.
    /// </summary>
    public class UiSetting : MonoBehaviour
    {
        /// <summary>
        /// The controls settings screen.
        /// </summary>
        [System.Serializable]
        public class ControlsUI
        {
            /// <summary>
            /// The min camera rotation sensitive, can't be greater than <see cref="MaxRotationSensitive"/>.
            /// </summary>
            [Min(0.1f)] public float MinRotationSensitive;

            /// <summary>
            /// The max camera rotation sensitive, can't be less than <see cref="MinRotationSensitive"/>.
            /// </summary>
            [Min(0.2f)] public float MaxRotationSensitive;

            /// <summary>
            /// The camera rotation sensitive UI slider.
            /// </summary>
            public Slider RotationSensitive;

            /// <summary>
            /// The toggle to invert the camera vertical orientation.
            /// </summary>
            public Toggle InvertVertical;

            /// <summary>
            /// The toggle to invert the camera horizontal orientation.
            /// </summary>
            public Toggle InvertHorizontal;

            public ControlsUI()
            {
                MinRotationSensitive = 0.1f;
                MaxRotationSensitive = 10f;
            }

            internal void Setup()
            {
                if (RotationSensitive)
                {
                    RotationSensitive.minValue = MinRotationSensitive;
                    RotationSensitive.maxValue = MaxRotationSensitive;
                    RotationSensitive.value = JUGameSettings.CameraSensibility;
                    RotationSensitive.onValueChanged.AddListener(OnChangeCameraSensitive);
                }

                if (InvertVertical)
                {
                    InvertVertical.isOn = JUGameSettings.CameraInvertVertical;
                    InvertVertical.onValueChanged.AddListener(OnToggleInvertCameraVertical);
                }

                if (InvertHorizontal)
                {
                    InvertHorizontal.isOn = JUGameSettings.CameraInvertHorizontal;
                    InvertHorizontal.onValueChanged.AddListener(OnToggleInvertCameraHorizontal);
                }
            }

            private void OnChangeCameraSensitive(float sensitive)
            {
                JUGameSettings.CameraSensibility = sensitive;
            }

            private void OnToggleInvertCameraVertical(bool invert)
            {
                JUGameSettings.CameraInvertVertical = invert;
            }

            private void OnToggleInvertCameraHorizontal(bool invert)
            {
                JUGameSettings.CameraInvertHorizontal = invert;
            }
        }

        /// <summary>
        /// The graphics settings screen.
        /// </summary>
        [System.Serializable]
        public class GraphicsUI
        {
            /// <summary>
            /// The min render scale allowed, can't be greater than <see cref="MaxRenderScale"/>.
            /// </summary>
            [Min(0.1f)] public float MinRenderScale;

            /// <summary>
            /// The max render scale allowed, can't be less than <see cref="MinRenderScale"/>
            /// </summary>
            [Min(0.2f)] public float MaxRenderScale;

            /// <summary>
            /// The quality settings UI dropdown.
            /// </summary>
            public Dropdown Quality;

            /// <summary>
            /// The render scale UI slider.
            /// </summary>
            public Slider RenderScale;

            public GraphicsUI()
            {
                MinRenderScale = 0.25f;
                MaxRenderScale = 1;
            }

            internal void Setup()
            {
                if (Quality)
                {
                    Quality.value = JUGameSettings.GraphicsQuality;
                    Quality.onValueChanged.AddListener(OnChangeQuality);
                }

                if (RenderScale)
                {
                    RenderScale.minValue = MinRenderScale;
                    RenderScale.maxValue = MaxRenderScale;
                    RenderScale.value = JUGameSettings.RenderScale;
                    RenderScale.onValueChanged.AddListener(OnChangeRenderScale);
                }
            }

            private void OnChangeQuality(int qualityIndex)
            {
                JUGameSettings.GraphicsQuality = qualityIndex;
            }

            private void OnChangeRenderScale(float scale)
            {
                JUGameSettings.RenderScale = scale;
            }
        }

        /// <summary>
        /// Use inputs to exit the settings screen instead of using UI buttons.
        /// </summary>
        public MultipleActionEvent CloseScreenAction;

        /// <summary>
        /// The exit settings screen UI button.
        /// </summary>
        public Button ExitButton;

        /// <summary>
        /// An event called when the screen is closed.
        /// </summary>
        public UnityEvent OnClose;

        /// <summary>
        /// The controls settings screen.
        /// </summary>
        public ControlsUI ControlsScreen;

        /// <summary>
        /// The graphics settings screen.
        /// </summary>
        public GraphicsUI GraphicsScreen;

        private void Awake()
        {
            Setup();
        }

        private void OnEnable()
        {
            CloseScreenAction.Enable();
        }

        private void OnDisable()
        {
            CloseScreenAction.Disable();
        }

        private void Setup()
        {
            if (ExitButton)
                ExitButton.onClick.AddListener(OnPressExitButton);

            CloseScreenAction.OnButtonsDown.AddListener(OnPressExitButton);

            ControlsScreen.Setup();
            GraphicsScreen.Setup();
        }

        private void OnPressExitButton()
        {
            Close();
        }

        /// <summary>
        /// Close the settings screen if it is open.
        /// </summary>
        public void Close()
        {
            if (!gameObject.activeSelf)
                return;

            gameObject.SetActive(false);
            OnClose.Invoke();
        }
    }
}


