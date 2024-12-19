using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using QuestsSystem;

public class TutorialManager : MonoBehaviour
{
    public GameObject panel; // Panel chứa nội dung hướng dẫn
    public TextMeshProUGUI instructionText; // Text để hiển thị nội dung hướng dẫn
    private Collider tutorialCollider; // Collider của game object
    public GameObject shootTriggerChild; // GameObject con của ShootTrigger
    private bool isTriggered = false; // Biến kiểm tra đã kích hoạt

    private void Start()
    {
        // Lưu trữ thành phần collider
        tutorialCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            // Kiểm tra từng trigger và hiển thị hướng dẫn tương ứng
            if (gameObject.name == "MoveTrigger")
            {
                ShowPanel("Move: Use WASD keys");
            }
            else if (gameObject.name == "ShootTrigger")
            {
                ShowPanel("Shoot or Attack: Use left mouse button");
                if (shootTriggerChild != null)
                {
                    Destroy(shootTriggerChild); // Phá hủy gameobject con nếu tồn tại
                }
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
            
            // Vô hiệu hóa collider sau khi kích hoạt
            tutorialCollider.enabled = false;
            StartCoroutine(HidePanelAfterDelay(5)); // Ẩn panel sau 5 giây
        }
    }

    private void ShowPanel(string instruction)
    {
        panel.SetActive(true); // Hiển thị panel
        instructionText.text = instruction; // Cập nhật nội dung hướng dẫn

        // Kiểm tra xem có nhiệm vụ nào đang hoạt động hay không
        if (QuestsManager.Instance.IsQuestActive(QuestsNames.Tutorial))
        {
            QuestsManager.Instance.UpdateQuestProgress(QuestsNames.Tutorial);
        }
    }

    private void HidePanel()
    {
        panel.SetActive(false); // Ẩn panel
    }

    private IEnumerator HidePanelAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        HidePanel();
    }
}
