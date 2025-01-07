using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Khoảng cách tối đa để tương tác
    public float interactionDistance;

    // Text hoặc crosshair để thông báo tương tác
    public GameObject interactionText;

    // Các lớp raycast có thể va chạm
    public LayerMask interactionLayers;

    // Danh sách các waypoint
    public List<GameObject> waypoints;

    void Update()
    {
        // Tạo biến lưu thông tin đối tượng raycast va chạm
        RaycastHit hit;

        // Vẽ raycast trong Scene view (chỉ dùng cho phát triển)
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);

        // Thực hiện raycast
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            // Kiểm tra nếu đối tượng có component Letter
            if (hit.collider.gameObject.GetComponent<Letter>())
            {
                // Hiển thị interaction text
                interactionText.SetActive(true);

                // Nếu nhấn phím F
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Mở hoặc đóng lá thư
                    Letter letter = hit.collider.gameObject.GetComponent<Letter>();
                    letter.openCloseLetter();

                    // Kiểm tra và phá hủy waypoint tương ứng nếu nó đang hoạt động
                    for (int i = 0; i < waypoints.Count; i++)
                    {
                        if (waypoints[i] != null && waypoints[i].activeSelf)
                        {
                            Destroy(waypoints[i]);
                            waypoints[i] = null; // Gán null để tránh lỗi MissingReferenceException
                            break; // Dừng lại sau khi phá hủy waypoint đầu tiên
                        }
                    }
                }
            }
            else
            {
                // Ẩn interaction text
                interactionText.SetActive(false);
            }
        }
        else
        {
            // Ẩn interaction text
            interactionText.SetActive(false);
        }
    }
}
