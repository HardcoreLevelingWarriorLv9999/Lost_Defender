using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public float detectionRadius = 5.0f; // Phạm vi phát hiện
    private Transform playerTransform; // Vị trí của người chơi

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= detectionRadius)
            {
                // Kiểm tra và phá hủy các đối tượng được gắn script Gas_Interactive
                DestroyGasInteractiveItemsInRange();
            }
        }
    }

    void DestroyGasInteractiveItemsInRange()
    {
        // Tìm tất cả các đối tượng trong phạm vi detectionRadius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Gas_Interactive gasInteractive = hitCollider.GetComponent<Gas_Interactive>();
            if (gasInteractive != null && !IsChildOfPlayer(hitCollider.transform))
            {
                Destroy(hitCollider.gameObject);
            }
        }
    }

    bool IsChildOfPlayer(Transform itemTransform)
    {
        Transform parent = itemTransform.parent;
        while (parent != null)
        {
            if (parent == playerTransform)
            {
                return true;
            }
            parent = parent.parent;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
