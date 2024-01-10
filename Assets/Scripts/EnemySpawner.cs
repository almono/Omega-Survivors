using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform minSpawnPoint, maxSpawnPoint;
    public float timeToSpawn = 2f;

    private float spawnCounter, despawnDistance;
    private Transform playerTarget;
    private List<GameObject> enemyList = new List<GameObject>();

    public int checkPerFrame = 30; // limit amount of enemies checked every frame
    private int enemyToCheck; // index of currently checked enemy

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn; // add small delay before spawning first enemy
        playerTarget = PlayerHealthController.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 2f; // allowed distance before we start despawning enemies
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCounter > 0)
        {
            spawnCounter -= Time.deltaTime;
        } else if (spawnCounter <= 0 && playerTarget)
        {
            // make sure player exists to spawn enemies ( in case we are on death screen for too long )
            spawnCounter = timeToSpawn;
            SpawnEnemy();
        }

        if(playerTarget)
        {
            transform.position = playerTarget.position;
        }

        DespawnFarEnemies();
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetSpawnPointPosition();
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint, transform.rotation);
        enemyList.Add(newEnemy); // add new enemy to the list
    }

    private void DespawnFarEnemies()
    {
        int checkTarget = enemyToCheck + checkPerFrame; // final enemy to check in this iteration

        // pick a segment of enemies to check per frame
        while (enemyToCheck < checkTarget)
        {
            if(enemyToCheck < enemyList.Count)
            {
                // if enemy exists
                if(enemyList[enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position, enemyList[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(enemyList[enemyToCheck]); // first destroy that enemy
                        enemyList.RemoveAt(enemyToCheck); // then remove it from the list
                        checkTarget--;
                    } else
                    {
                        enemyToCheck++; // enemy found and checked, move to the next enemy to check
                    }
                } else
                {
                    // if the enemy does not exist ( has been killed ) remove it from the list and update checkTarget
                    enemyList.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            } else
            {
                enemyToCheck = 0; // loop back
                checkTarget = 0;
            }
        }
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
