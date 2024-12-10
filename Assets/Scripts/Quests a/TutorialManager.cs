using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using QuestsSystem;

public class TutorialManager : MonoBehaviour
{
    public GameObject panel; // Panel containing tutorial content
    public TextMeshProUGUI instructionText; // Text to display tutorial content
    private Collider tutorialCollider; // Collider of the game object

    private void Start()
    {
        // Cache the collider component
        tutorialCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check each trigger and display the appropriate tutorial
            if (gameObject.name == "MoveTrigger")
            {
                ShowPanel("Move: Use WASD keys");
            }
            else if (gameObject.name == "ShootTrigger")
            {
                ShowPanel("Shoot or Attack: Use left mouse button");
            }
            else if (gameObject.name == "JumpTrigger")
            {
                ShowPanel("Jump: Press the Space key");
            }
            else if (gameObject.name == "RunTrigger")
            {
                ShowPanel("Run: Press the Shift key");
            }
            else if (gameObject.name == "InventoryTrigger")
            {
                ShowPanel("Open Inventory: Press the Tab key");
            }
            
            // Disable the collider after triggering
            tutorialCollider.enabled = false;
            StartCoroutine(HidePanelAfterDelay(7)); // Hide the panel after 5 seconds
        }
    }

    private void ShowPanel(string instruction)
    {
        panel.SetActive(true); // Show the panel
        instructionText.text = instruction; // Update tutorial content

        // Check for quest existing in active quests
        if (QuestsManager.Instance.IsQuestActive(QuestsNames.Tutorial))
        {
            QuestsManager.Instance.UpdateQuestProgress(QuestsNames.Tutorial);
        }
    }

    private void HidePanel()
    {
        panel.SetActive(false); // Hide the panel
    }

    private IEnumerator HidePanelAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HidePanel();
        Destroy(gameObject);
    }
}
