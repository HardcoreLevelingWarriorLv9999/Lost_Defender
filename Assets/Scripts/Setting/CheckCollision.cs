using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] string Audio;
    [SerializeField] string longCollisionAudio;
    private float collisionTime = 0f;
    private bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
            AudioManager.Instance.PlaySFX(Audio);
            Debug.Log("Va chạm bắt đầu!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            collisionTime = 0f;
            AudioManager.Instance.StopSFX(Audio);
            AudioManager.Instance.StopSFX(longCollisionAudio);
            Debug.Log("Va chạm kết thúc!");
        }
    }

    private void Update()
    {
        if (isColliding)
        {
            collisionTime += Time.deltaTime;
            if (collisionTime >= 2f)
            {
                AudioManager.Instance.StopSFX(Audio);
                AudioManager.Instance.PlaySFX(longCollisionAudio);
                Debug.Log("Va chạm kéo dài trên 2 giây!");
                // Đặt isColliding thành false để chỉ phát nhạc một lần
                isColliding = false;
            }
        }
    }

}
