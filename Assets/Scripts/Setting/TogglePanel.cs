using UnityEngine;
using UnityEngine.EventSystems;

public class TogglePanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject panel; // Panel bạn muốn hiển thị hoặc ẩn

    // Gọi khi người chơi chạm vào button
    public void OnPointerDown(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    // Gọi khi người chơi không còn chạm vào button
    public void OnPointerUp(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
