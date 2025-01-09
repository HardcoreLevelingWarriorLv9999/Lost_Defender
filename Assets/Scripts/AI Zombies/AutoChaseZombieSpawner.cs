using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoChaseZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider[] spawnAreas;  // Use an array of Colliders
    public List<GameObject> spawnedZombies = new List<GameObject>();

    public void SpawnRandomZombies(int count)
    {
        // First, ensure at least one zombie is spawned in each BoxCollider
        foreach (Collider spawnArea in spawnAreas)
        {
            if (count <= 0) break;
            SpawnRandomZombieInArea(spawnArea);
            count--;
        }

        // Spawn remaining zombies randomly
        for (int i = 0; i < count; i++)
        {
            SpawnRandomZombie();
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

    public void DestroyAllZombies()
    {
        foreach (GameObject zombie in spawnedZombies)
        {
            if (zombie != null)
            {
                Destroy(zombie); // Phá hủy zombie trong scene
            }
        }
        spawnedZombies.Clear(); // Xóa danh sách zombie
    }

}
