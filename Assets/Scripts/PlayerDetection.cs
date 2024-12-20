using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Collider targetCollider; // Collider mà bạn muốn bật/tắt
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

        if (targetCollider == null)
        {
            Debug.LogError("Target Collider is not assigned!");
        }
    }

    void Update()
    {
        if (playerTransform != null && targetCollider != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= detectionRadius)
            {
                targetCollider.enabled = false;
            }
            else
            {
                targetCollider.enabled = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
