using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float firstPhaseTime = 30f; // Thời gian giai đoạn đầu (30 giây)
    [SerializeField] float secondPhaseTime = 120f; // Thời gian giai đoạn thứ hai (2 phút)
    [SerializeField] GameObject winPanel; // Tham chiếu tới panel chiến thắng
    public float remainingTime;
    private bool isFirstPhase = true;
    private bool isTimeUp = false;
    private bool isCountingDown = false;
    public AutoChaseZombieSpawner zombieSpawner;
    int openMap1;

    void Start()
    {
        remainingTime = firstPhaseTime;
        winPanel.SetActive(false); // Đảm bảo panel chiến thắng được ẩn ban đầu
        LoadPlayerData();
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
                    remainingTime = secondPhaseTime;
                    isFirstPhase = false;
                    StartCoroutine(SpawnZombiesWithDelay());
                }
                else
                {
                    isTimeUp = true;
                    StopCoroutine(SpawnZombiesWithDelay());
                    ShowWinPanel(); // Hiện panel chiến thắng
                    int difficulty = PlayerPrefs.GetInt("Difficulty");
                  
                    if (difficulty == 0 && openMap1==0)
                    {
                        SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
                        string nameGame = PlayerPrefs.GetString("FileGame");
                        data.openMap1 = 1;
                        SaveLoadManager.SaveData(data);
                    }
                    else if(difficulty == 1 && openMap1==1)
                    {
                        SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
                        string nameGame = PlayerPrefs.GetString("FileGame");
                        data.openMap1 = 2;
                        SaveLoadManager.SaveData(data);
                    }
                    else if (difficulty == 2 && openMap1 == 2)
                    {
                        SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
                        string nameGame = PlayerPrefs.GetString("FileGame");
                        data.openMap1 = 2;
                        SaveLoadManager.SaveData(data);
                    }
                    else if (difficulty == 3 && openMap1 == 3)
                    {
                        SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
                        string nameGame = PlayerPrefs.GetString("FileGame");
                        data.openMap1 = 2;
                        SaveLoadManager.SaveData(data);
                    }

                }
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartCountdown()
    {
        isCountingDown = true;
    }

    IEnumerator SpawnZombiesWithDelay()
    {
        while (!isTimeUp)
        {
            zombieSpawner.SpawnRandomZombies(Random.Range(1, 11));
            yield return new WaitForSeconds(5f);
        }
    }

    void ShowWinPanel()
    {
        winPanel.SetActive(true); // Hiện panel chiến thắng
    }
    void LoadPlayerData()
    {

        SaveLoadManager.PlayerData data = SaveLoadManager.LoadData();
        if (data != null)
        {
           
          openMap1 = data.openMap1;
          

        }
        else
        {
            Debug.LogError("Không thể tải dữ liệu người chơi!");
        }
    }
}
