using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float firstPhaseTime = 30f; // Thời gian giai đoạn đầu (30 giây)
    [SerializeField] float secondPhaseTime = 120f; // Thời gian giai đoạn thứ hai (2 phút)
    public float remainingTime;
    private bool isFirstPhase = true;
    private bool isTimeUp = false;
    private bool isCountingDown = false; // Thêm biến này
    public AutoChaseZombieSpawner zombieSpawner; // Thêm tham chiếu tới script AutoChaseZombieSpawner

    void Start()
    {
        // Ban đầu đặt remainingTime thành firstPhaseTime
        remainingTime = firstPhaseTime;
    }

    void Update()
    {
        if (isCountingDown && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                if (isFirstPhase)
                {
                    // Chuyển sang giai đoạn thứ hai
                    remainingTime = secondPhaseTime;
                    isFirstPhase = false;
                    StartCoroutine(SpawnZombiesWithDelay()); // Bắt đầu spawn zombie mỗi 5 giây
                }
                else
                {
                    isTimeUp = true;
                    StopCoroutine(SpawnZombiesWithDelay()); // Dừng việc spawn zombie
                }
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartCountdown()
    {
        isCountingDown = true; // Gọi hàm này để bắt đầu đếm ngược
    }

    IEnumerator SpawnZombiesWithDelay()
    {
        while (!isTimeUp)
        {
            zombieSpawner.SpawnRandomZombies(Random.Range(1, 11)); // Spawn từ 1 đến 10 zombie
            yield return new WaitForSeconds(5f); // Đợi 5 giây trước khi spawn lần tiếp theo
        }
    }
}
