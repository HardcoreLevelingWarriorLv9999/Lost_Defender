using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 3.0f; // Maximum stamina in seconds
    public float currentStamina;
    public float staminaRegenRate = 1.0f; // Stamina regeneration rate per second
    public Image staminaFill; // Reference to the UI Image representing the fill

    private bool isSprinting = false;
    private CharacterController characterController;

    void Start()
    {
        currentStamina = maxStamina;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                StartSprinting();
            }
            else
            {
                StopSprinting();
            }
        }
        else
        {
            StopSprinting();
        }

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            RegenerateStamina();
        }

        UpdateStaminaUI();
    }

    void StartSprinting()
    {
        isSprinting = true;
        currentStamina -= Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Add your sprinting logic here (e.g., increase movement speed)
    }

    void StopSprinting()
    {
        isSprinting = false;

        // Add your logic to stop sprinting here (e.g., revert to normal movement speed)
    }

    void RegenerateStamina()
    {
        currentStamina = maxStamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    void UpdateStaminaUI()
    {
        if (staminaFill != null)
        {
            staminaFill.fillAmount = currentStamina / maxStamina;
        }
    }
}
