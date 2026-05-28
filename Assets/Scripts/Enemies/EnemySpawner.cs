using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public Transform[] spawnPoints;

    public int currentLevel = 1;

    void Start()
    {
       // SpawnLevel(); 
    }

    public void SpawnLevel()
    {
        int pattern =
            UnityEngine.Random.Range(0, 3);

        switch (pattern)
        {
            case 0:
                SpawnCirclePattern();
                break;

            case 1:
                SpawnLinePattern();
                break;

            case 2:
                SpawnRandomPattern();
                break;
        }

    }

    // =========================
    // PATTERN 1
    // Circle around player
    // =========================

    void SpawnCirclePattern()
    {
        int enemyCount = 5 + currentLevel;

        Vector3 center = Vector3.zero;

        float radius = 8f;

        for (int i = 0; i < enemyCount; i++)
        {
            float angle =
                i * Mathf.PI * 2 / enemyCount;

            Vector3 pos =
                center +
                new Vector3(
                    Mathf.Cos(angle),
                    Mathf.Sin(angle),
                    0
                ) * radius;

            SpawnRandomEnemy(pos);
        }
    }

    // =========================
    // PATTERN 2
    // Enemy wall
    // =========================

    void SpawnLinePattern()
    {
        int enemyCount = 6 + currentLevel;

        float spacing = 2f;

        Vector3 startPos =
            new Vector3(-enemyCount, 8f, 0);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos =
                startPos +
                new Vector3(i * spacing, 0, 0);

            SpawnRandomEnemy(pos);
        }
    }

    // =========================
    // PATTERN 3
    // Random positions
    // =========================

    void SpawnRandomPattern()
    {
        int enemyCount = 4 + currentLevel;

        for (int i = 0; i < enemyCount; i++)
        {
            Transform point =
                spawnPoints[
                    UnityEngine.Random.Range(0, spawnPoints.Length)
                ];

            SpawnRandomEnemy(point.position);
        }
    }

    void SpawnRandomEnemy(Vector3 position)
    {
        GameObject prefab =
            enemyPrefabs[
                UnityEngine.Random.Range(0, enemyPrefabs.Length)
            ];

        Instantiate(
            prefab,
            position,
            Quaternion.identity
        );
    }
}