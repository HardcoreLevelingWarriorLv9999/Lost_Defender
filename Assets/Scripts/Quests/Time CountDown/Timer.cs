using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float firstPhaseTime = 30f;
    [SerializeField] float secondPhaseTime = 120f;
    [SerializeField] GameObject winPanel;
    public float remainingTime;
    private bool isFirstPhase = true;
    private bool isTimeUp = false;
    private bool isCountingDown = false;
    public AutoChaseZombieSpawner autoChaseZombieSpawner;
    public ZombieSpawner zombieSpawner;

    void Start()
    {
        remainingTime = firstPhaseTime;
        winPanel.SetActive(false);
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
                    zombieSpawner.DestroyAllZombies();
                    zombieSpawner.DisableSpawning();
                }
                else
                {
                    isTimeUp = true;
                    StopCoroutine(SpawnZombiesWithDelay());
                    autoChaseZombieSpawner.DestroyAllZombies();
                    ShowWinPanel();

                    SaveLoadManager.PlayerData data = new SaveLoadManager.PlayerData();
                    string nameGame = PlayerPrefs.GetString("FileGame");
                    data.openMap1 = 1;
                    SaveLoadManager.SaveData(data);
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
            if (autoChaseZombieSpawner.spawnedZombies.Count < 20)
            {
                int remainingSlots = 20 - autoChaseZombieSpawner.spawnedZombies.Count;
                int zombiesToSpawn = Random.Range(1, Mathf.Min(11, remainingSlots + 1));
                autoChaseZombieSpawner.SpawnRandomZombies(zombiesToSpawn);
            }
            yield return new WaitForSeconds(10f);
        }
    }

    void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
}
