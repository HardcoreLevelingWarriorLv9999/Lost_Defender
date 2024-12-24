using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import thư viện TextMeshPro
using JUTPS;

namespace JUTPS.UI
{

    [AddComponentMenu("JU TPS/UI/UI Vehicle Health Bar")]
    public class UIVehicleHealthBar : MonoBehaviour
    {
        [Header("UI Vehicle Health Bar Settings")]
        [SerializeField] private GameObject VehicleObject;
        [SerializeField] private JUHealth HealthComponent;
        [SerializeField] private Slider HealthBarSlider;  // Sử dụng Slider thay vì Image
        [SerializeField] private float Speed = 6;
        [SerializeField] private TextMeshProUGUI HealthPointsText;  // Sử dụng TextMeshProUGUI thay vì Text

        [Header("Health Bar Color Change")]
        [SerializeField] private Color EmptyHPColor = Color.red;
        [SerializeField] private Color FullHPColor = Color.green;
        [SerializeField] private Color HPHealingColor = Color.cyan;
        [SerializeField] private Color HPLossColor = Color.yellow;
        [SerializeField] private bool ChangeHPTextColorToo = true;

        private float oldFillAmount;

        void Start()
        {
            if (VehicleObject != null)
            {
                HealthComponent = VehicleObject.GetComponent<JUHealth>();
            }

            oldFillAmount = HealthBarSlider.value;
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

            if (oldFillAmount != HealthBarSlider.value)
            {
                //Health Healing
                if (oldFillAmount < HealthBarSlider.value)
                {
                    HealthBarSlider.fillRect.GetComponentInChildren<Image>().color = HPHealingColor;
                }
                //Health Loss
                if (oldFillAmount > HealthBarSlider.value)
                {
                    HealthBarSlider.fillRect.GetComponentInChildren<Image>().color = HPLossColor;
                }

                oldFillAmount = HealthBarSlider.value;
            }

        }
    }

}
