using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform minSpawnPoint, maxSpawnPoint;
    public float timeToSpawn = 2f;
    private float spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn; // add small delay before spawning first enemy
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCounter > 0)
        {
            spawnCounter -= Time.deltaTime;
        } else if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        //Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        Vector3 spawnPoint = GetSpawnPointPosition();
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint, transform.rotation);
        //RotateSprite(newEnemy);
    }

    private Vector3 GetSpawnPointPosition()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if(spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);

            if(Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.x = maxSpawnPoint.position.x;
            } else
            {
                spawnPoint.x = minSpawnPoint.position.x;
            }
        } else
        {
            spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = maxSpawnPoint.position.y;
            }
            else
            {
                spawnPoint.y = minSpawnPoint.position.y;
            }
        }

        return spawnPoint;
    }
}
