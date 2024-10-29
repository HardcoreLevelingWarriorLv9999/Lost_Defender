using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider spawnArea;
    public int numberOfZombies = 1;

    public List<GameObject> spawnedZombies = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            SpawnRandomZombie();
        }
    }

    void SpawnRandomZombie()
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
