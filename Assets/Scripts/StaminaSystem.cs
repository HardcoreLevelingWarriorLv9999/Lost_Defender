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
    private float timeSinceExhausted = 0.0f; // Time since stamina reached 0
    private bool isExhausted = false; // Whether the player is exhausted

    void Start()
    {
        currentStamina = maxStamina;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            if (isRunning && currentStamina > 0)
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

        HandleStaminaRegeneration(isRunning);
        UpdateStaminaUI();
    }

    void StartSprinting()
    {
        isSprinting = true;
        currentStamina -= Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina == 0)
        {
            isExhausted = true;
            timeSinceExhausted = 0.0f;
        }

        // Add your sprinting logic here (e.g., increase movement speed)
    }

    void StopSprinting()
    {
        isSprinting = false;

        // Add your logic to stop sprinting here (e.g., revert to normal movement speed)
    }

    void HandleStaminaRegeneration(bool isRunning)
    {
        if (isExhausted)
        {
            // Wait for 2 seconds before starting regeneration
            timeSinceExhausted += Time.deltaTime;
            if (timeSinceExhausted >= 2.0f && !isRunning)
            {
                RegenerateStamina();
            }
        }
        else
        {
            if (!isRunning) // Regenerate stamina only if not running
            {
                RegenerateStamina();
            }
        }
    }

    void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina == maxStamina)
            {
                isExhausted = false; // Reset exhaustion state when stamina is full
            }
        }
    }

    void UpdateStaminaUI()
    {
        if (staminaFill != null)
        {
            staminaFill.fillAmount = currentStamina / maxStamina;
        }
    }
}
