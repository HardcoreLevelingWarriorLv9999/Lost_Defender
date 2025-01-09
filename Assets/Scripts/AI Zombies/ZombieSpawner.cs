using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider[] spawnAreas;
    public float detectionRadius = 20.0f;

    private List<GameObject> spawnedZombies = new List<GameObject>();
    private Transform playerTransform;
    private bool[] areaHasSpawned;
    private int[] zombiesToSpawn;
    private bool isSpawningEnabled = true;

    void Start()
    {
        areaHasSpawned = new bool[spawnAreas.Length];
        zombiesToSpawn = new int[spawnAreas.Length];

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }

        for (int i = 0; i < spawnAreas.Length; i++)
        {
            zombiesToSpawn[i] = Random.Range(1, 6);
        }
    }

    void Update()
    {
        if (isSpawningEnabled && playerTransform != null)
        {
            for (int i = 0; i < spawnAreas.Length; i++)
            {
                if (!areaHasSpawned[i] && Vector3.Distance(playerTransform.position, spawnAreas[i].transform.position) <= detectionRadius)
                {
                    for (int j = 0; j < zombiesToSpawn[i]; j++)
                    {
                        SpawnRandomZombieInArea(spawnAreas[i]);
                    }
                    areaHasSpawned[i] = true;
                }
            }
        }
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
                Destroy(zombie);
            }
        }
        spawnedZombies.Clear();
    }

    public void DisableSpawning()
    {
        isSpawningEnabled = false;
    }

    void OnDrawGizmos()
    {
        if (spawnAreas != null)
        {
            Gizmos.color = Color.green;
            foreach (Collider spawnArea in spawnAreas)
            {
                Gizmos.DrawWireSphere(spawnArea.transform.position, detectionRadius);
            }
        }
    }
}