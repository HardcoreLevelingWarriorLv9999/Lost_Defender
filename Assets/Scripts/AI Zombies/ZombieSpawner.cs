using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider[] spawnAreas;  // Use an array of Colliders
    public int numberOfZombies = 10;  // Total number of zombies to spawn

    public List<GameObject> spawnedZombies = new List<GameObject>();

    void Start()
    {
        // First, ensure at least one zombie is spawned in each BoxCollider
        foreach (Collider spawnArea in spawnAreas)
        {
            SpawnRandomZombieInArea(spawnArea);
        }

        // Calculate remaining zombies to spawn randomly
        int remainingZombies = numberOfZombies - spawnAreas.Length;

        for (int i = 0; i < remainingZombies; i++)
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
}
