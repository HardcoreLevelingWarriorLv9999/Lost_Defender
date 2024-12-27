using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace
using JUTPS;

namespace JUTPS.UI
{
    [AddComponentMenu("JU TPS/UI/UI Health Bar")]
    public class UIHealhBar : MonoBehaviour
    {
        [Header("UI Health Bar Settings")]
        [SerializeField] private JUHealth HealthComponent;
        [SerializeField] private bool IsPlayerHealthBar = true;
        [SerializeField] private Slider HealthBarSlider;  // Use Slider instead of Image
        [SerializeField] private float Speed = 6;
        [SerializeField] private TextMeshProUGUI HealthPointsText;  // Use TextMeshProUGUI

        [Header("Health Bar Color Change")]
        [SerializeField] private Color EmptyHPColor = Color.red;
        [SerializeField] private Color FullHPColor = Color.green;
        [SerializeField] private Color HPHealingColor = Color.cyan;
        [SerializeField] private Color HPLossColor = Color.yellow;
        [SerializeField] private bool ChangeHPTextColorToo = true;

        private float oldValue;
        void Start()
        {
            if (IsPlayerHealthBar)
            {
                GameObject pl = GameObject.FindGameObjectWithTag("Player");
                HealthComponent = pl.GetComponent<JUHealth>();
            }

            oldValue = HealthBarSlider.value;
        }

        void Update()
        {
            if (HealthComponent == null || HealthBarSlider == null) return;

            float healthValueNormalized = HealthComponent.Health / HealthComponent.MaxHealth;
            HealthBarSlider.value = Mathf.MoveTowards(HealthBarSlider.value, healthValueNormalized, Speed * Time.deltaTime);

            HealthBarSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(EmptyHPColor, FullHPColor, HealthBarSlider.value);

            if (HealthPointsText != null)
            {
                HealthPointsText.text = HealthComponent.Health.ToString("000") + "/" + HealthComponent.MaxHealth;
                if (ChangeHPTextColorToo) HealthPointsText.color = Color.Lerp(HealthBarSlider.fillRect.GetComponentInChildren<Image>().color, Color.white, 0.6f);
            }
            if (oldValue != HealthBarSlider.value)
            {
                //Health Healing
                if (oldValue < HealthBarSlider.value)
                {
                    HealthBarSlider.fillRect.GetComponentInChildren<Image>().color = HPHealingColor;
                }
                //Health Loss
                if (oldValue > HealthBarSlider.value)
                {
                    HealthBarSlider.fillRect.GetComponentInChildren<Image>().color = HPLossColor;
                }

                oldValue = HealthBarSlider.value;
            }
        }
    }
}
