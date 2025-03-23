using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Main;

    private Transform playerTransform;

    public List<GameObject> SafePrefabs;
    public List<GameObject> EnemyPrefabs;

    public AnimationCurve EnemyRatio;
    public Transform FurthestEnemySpawnTransform;
    public Transform PlayerSpawnTransform;
    public float roadRadius = 10f;
    public float SpawnOffsetFromPlayer = 30f;

    private float furthestEnemySpawnX;
    private float timeSinceLastSpawn;
    private float timeSinceBeginning;
    private float furthestPlayerX = float.MinValue;

    private void Awake()
    {
        if (Main)
        {
            Debug.LogError($"EnemySpawner.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;
    }

    private void Start()
    {
        playerTransform = DangerSystem.Main.transform;
        timeSinceBeginning = 0;
        furthestEnemySpawnX = FurthestEnemySpawnTransform.position.x;
    }

    void Update()
    {
        furthestPlayerX = Mathf.Max(furthestPlayerX, playerTransform.position.x);
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceBeginning += Time.deltaTime;

        if (timeSinceLastSpawn > CalculateAffluence(timeSinceBeginning))
        {
            Spawn();
        }
    }

    public float CalculateAffluence(float time)
    {
        time %= 20f; // creneau period
        if (time < 15f) // time for high creneau
        {
            return 1f;
        }
        return 2f; // time for low creneau
    }
    
    public void Spawn()
    {
        int spawnCount = Random.Range(1, 4);

        float zPos = float.MaxValue;
        for (int i = 0; i < spawnCount; i++)
        {
            if (zPos != float.MaxValue)
            {
                // Spawning next to same generation member
                zPos += 0.2f;
            }
            else
            {
                zPos = Random.Range(-0.8f, 1f - 0.2f * spawnCount) * roadRadius;
            }

            Vector3 randomSpawnPos = new(GetSpawnX(), FurthestEnemySpawnTransform.position.y, zPos);
            Quaternion spawnRotation = Quaternion.identity;

            if (Random.Range(0f, 1f) < EnemyRatio.Evaluate(timeSinceBeginning))
            {
                Instantiate(GetRandomPrefab(EnemyPrefabs), randomSpawnPos, spawnRotation);
            }
            else
            {
                Instantiate(GetRandomPrefab(SafePrefabs), randomSpawnPos, spawnRotation);
            }
        }

        timeSinceLastSpawn = 0;
    }

    private GameObject GetRandomPrefab(List<GameObject> prefabs)
    {
        return prefabs[Random.Range(0, prefabs.Count)];
    }

    public float GetSpawnX()
    {
        return Mathf.Min(furthestPlayerX + SpawnOffsetFromPlayer, furthestEnemySpawnX);
    }

    public void OnDrawGizmosSelected()
    {
        float x = GetSpawnX();
        Vector3 from = new(x, FurthestEnemySpawnTransform.position.y, roadRadius);
        Vector3 to = new(x, FurthestEnemySpawnTransform.position.y, -roadRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }
}
