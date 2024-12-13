using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public GameObject sparkleEffectObject; // GameObject để chạy animation lấp lánh
    public List<GameObject> targetGameObjects; // Danh sách các GameObject cần gắn waypoint
    public Vector3 offset;
    private float edgePadding = 40f; // Khoảng đệm từ cạnh màn hình
    private List<GameObject> waypointInstances; // Danh sách các waypoint đã tạo

    private void Start()
    {
        waypointInstances = new List<GameObject>();

        foreach (var target in targetGameObjects)
        {
            GameObject waypoint = Instantiate(sparkleEffectObject, sparkleEffectObject.transform.parent);
            waypointInstances.Add(waypoint);
            waypoint.SetActive(target.activeSelf); // Bật hoặc tắt waypoint dựa trên trạng thái của target
        }
    }

    private void Update()
    {
        // Duyệt ngược để dễ dàng xóa mục khỏi danh sách
        for (int i = targetGameObjects.Count - 1; i >= 0; i--)
        {
            GameObject target = targetGameObjects[i];
            GameObject waypoint = waypointInstances[i];

            if (target == null)
            {
                // Xóa waypoint và mục liên quan nếu target không còn tồn tại
                Destroy(waypoint);
                targetGameObjects.RemoveAt(i);
                waypointInstances.RemoveAt(i);
                continue;
            }

            // Cập nhật trạng thái của waypoint dựa trên trạng thái của target
            waypoint.SetActive(target.activeSelf);

            if (!target.activeSelf)
            {
                continue;
            }

            float minX = waypoint.GetComponent<RectTransform>().rect.width / 2 + edgePadding;
            float maxX = Screen.width - minX - edgePadding;
            float minY = waypoint.GetComponent<RectTransform>().rect.height / 2 + edgePadding;
            float maxY = Screen.height - minY - edgePadding;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position + offset);

            if (Vector3.Dot(target.transform.position - transform.position, transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            waypoint.transform.position = pos; // Cập nhật vị trí của waypoint
            TextMeshProUGUI meter = waypoint.GetComponentInChildren<TextMeshProUGUI>();
            meter.text = Vector3.Distance(target.transform.position, transform.position).ToString("0") + "m"; // Cập nhật giá trị văn bản mét
        }
    }
}
