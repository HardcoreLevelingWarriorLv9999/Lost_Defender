using System.Collections;
using System.Collections.Generic;
using JUTPS.Utilities;
using UnityEngine;

public class AutoChaseZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider[] spawnAreas;  // Sử dụng một mảng Collider
    public List<GameObject> spawnedZombies = new List<GameObject>();

    public void SpawnRandomZombies(int count)
    {
        // Đầu tiên, đảm bảo ít nhất một zombie được tạo ra trong mỗi BoxCollider
        foreach (Collider spawnArea in spawnAreas)
        {
            if (count <= 0) break;
            SpawnRandomZombieInArea(spawnArea);
            count--;
        }

        // Tạo ra các zombie còn lại một cách ngẫu nhiên
        for (int i = 0; i < count; i++)
        {
            if (spawnedZombies.Count < 20) // Kiểm tra số lượng zombie hiện tại
            {
                SpawnRandomZombie();
            }
        }
    }

    void SpawnRandomZombie()
    {
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        Vector3 spawnPosition = GetRandomPointInCollider(spawnAreas[Random.Range(0, spawnAreas.Length)]);
        GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity, transform);

        Animator animator = newZombie.GetComponent<Animator>();
        RuntimeAnimatorController randomController = zombieControllers[Random.Range(0, zombieControllers.Length)];
        animator.runtimeAnimatorController = randomController;

        spawnedZombies.Add(newZombie);
    }

    void SpawnRandomZombieInArea(Collider spawnArea)
    {
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        Vector3 spawnPosition = GetRandomPointInCollider(spawnArea);
        GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity, transform);

        Animator animator = newZombie.GetComponent<Animator>();
        RuntimeAnimatorController randomController = zombieControllers[Random.Range(0, zombieControllers.Length)];
        animator.runtimeAnimatorController = randomController;

        spawnedZombies.Add(newZombie);
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point;
        do
        {
            point = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        } while (!collider.bounds.Contains(point));
        return point;
    }
}
