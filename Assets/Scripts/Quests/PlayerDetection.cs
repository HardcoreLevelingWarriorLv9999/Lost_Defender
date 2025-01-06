using UnityEngine;
using QuestsSystem;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] string Audio;
    [SerializeField] string longCollisionAudio;
    public Collider targetCollider;
    public float detectionRadius = 5.0f;
    private Transform playerTransform;
    private bool isDetectionEnabled = true; // Thêm biến trạng thái
    private float collisionTime = 0f;
    private bool isColliding = false;
    private bool longAudioPlayed = false;

    private void OnEnable()
    {
        FuelUp_QuestLogic.OnQuestAccepted += DisableDetection;
        FuelUp_QuestLogic.OnQuestCompleted += EnableDetection;
    }

    private void OnDisable()
    {
        FuelUp_QuestLogic.OnQuestAccepted -= DisableDetection;
        FuelUp_QuestLogic.OnQuestCompleted -= EnableDetection;
    }

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
        if (!isDetectionEnabled) return; // Bỏ qua nếu phát hiện bị vô hiệu hóa

        if (playerTransform != null && targetCollider != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= detectionRadius)
            {
                if (!isColliding)
                {
                    isColliding = true;
                    collisionTime = 0f;
                    AudioManager.Instance.PlaySFX(Audio);
                    Debug.Log("Nhân vật vào phạm vi!");
                }

                collisionTime += Time.deltaTime;
                if (collisionTime >= 2f && !longAudioPlayed)
                {
                    AudioManager.Instance.StopSFX(Audio);
                    AudioManager.Instance.PlaySFX(longCollisionAudio);
                    longAudioPlayed = true;
                    Debug.Log("Nhân vật ở trong phạm vi trên 2 giây!");
                }
            }
            else
            {
                if (isColliding)
                {
                    isColliding = false;
                    collisionTime = 0f;
                    longAudioPlayed = false;
                    AudioManager.Instance.StopSFX(Audio);
                    AudioManager.Instance.StopSFX(longCollisionAudio);
                    Debug.Log("Nhân vật rời khỏi phạm vi!");
                }
            }

            targetCollider.enabled = !isColliding;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void DisableDetection()
    {
        isDetectionEnabled = false; // Thay vì tắt script, chỉ tắt logic phát hiện
        targetCollider.enabled = true;
     
        Debug.Log("PlayerDetection disabled");
    }

    private void EnableDetection()
    {
        isDetectionEnabled = true; // Bật lại logic phát hiện
        targetCollider.enabled = false;
        Debug.Log("PlayerDetection enabled");
    }

}
