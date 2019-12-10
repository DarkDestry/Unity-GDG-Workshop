using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0, 10)]
    public float spawnRate = 1.2f;

    private float timeSinceLastSpawn;

    public GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeSinceLastSpawn > spawnRate)
        {
            timeSinceLastSpawn = Time.time;

            int randomEnemyIndex = Random.Range(0, enemies.Length);

            Vector2 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-2.0f, 2.0f);

            Instantiate(enemies[randomEnemyIndex], spawnPosition, Quaternion.identity);
        }
    }
}
