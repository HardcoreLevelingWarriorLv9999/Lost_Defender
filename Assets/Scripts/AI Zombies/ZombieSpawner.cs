using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public RuntimeAnimatorController[] zombieControllers;
    public Collider[] spawnAreas;  // Các khu vực spawn
    public float detectionRadius = 20.0f;  // Phạm vi phát hiện người chơi

    private List<GameObject> spawnedZombies = new List<GameObject>();
    private Transform playerTransform;
    private bool[] areaHasSpawned;
    private int[] zombiesToSpawn; // Số lượng zombie cần spawn ở mỗi khu vực

    void Start()
    {
        // Khởi tạo mảng đánh dấu khu vực đã spawn và số lượng zombie cần spawn
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

        // Đảm bảo mỗi khu vực sẽ có ít nhất 1 zombie và tối đa 5 zombie
        for (int i = 0; i < spawnAreas.Length; i++)
        {
            zombiesToSpawn[i] = Random.Range(1, 6); // Số lượng zombie từ 1 đến 5
        }
    }

    void Update()
    {
        if (playerTransform != null)
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
