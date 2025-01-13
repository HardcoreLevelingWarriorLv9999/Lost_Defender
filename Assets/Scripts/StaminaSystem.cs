using JUTPS.CharacterBrain;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 3.0f; // Maximum stamina in seconds
    public float currentStamina;
    public float staminaRegenRate = 1.0f; // Stamina regeneration rate per second
    public Image staminaFill; // Reference to the UI Image representing the fill

    private bool isSprinting = false;
    private JUCharacterBrain characterBrain;
    private float timeSinceExhausted = 0.0f; // Time since stamina reached 0
    private bool isExhausted = false; // Whether the player is exhausted

    void Start()
    {
        currentStamina = maxStamina;
        characterBrain = GetComponent<JUCharacterBrain>();

        if (characterBrain == null)
        {
            Debug.LogError("JUCharacterBrain component not found on this GameObject");
        }
    }

    void Update()
    {
        if (characterBrain != null)
        {
            bool isMoving = characterBrain.IsMoving;
            bool isRunning = characterBrain.IsRunning;
            bool isSprintingKeyHeld = Input.GetKey(KeyCode.LeftShift); // Assuming Left Shift is the sprint key

            if (isMoving)
            {
                if (isRunning && isSprintingKeyHeld && currentStamina > 0)
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

            HandleStaminaRegeneration(isSprintingKeyHeld);
            UpdateStaminaUI();
        }
    }

    void StartSprinting()
    {
        if (!isSprinting) // Only start sprinting if not already sprinting
        {
            isSprinting = true;
        }

        if (isSprinting)
        {
            currentStamina -= Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina == 0)
            {
                isExhausted = true;
                timeSinceExhausted = 0.0f;
            }

            characterBrain.IsSprinting = true;
        }
    }

    void StopSprinting()
    {
        if (isSprinting) // Only stop sprinting if currently sprinting
        {
            isSprinting = false;
            characterBrain.IsSprinting = false;
        }
    }

    void HandleStaminaRegeneration(bool isSprintingKeyHeld)
    {
        if (isExhausted)
        {
            // Wait for 2 seconds before starting regeneration
            timeSinceExhausted += Time.deltaTime;
            if (timeSinceExhausted >= 2.0f && !isSprintingKeyHeld)
            {
                RegenerateStamina();
            }
        }
        else
        {
            if (!isSprintingKeyHeld) // Regenerate stamina only if sprint key not held
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
