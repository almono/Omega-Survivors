using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Destructible Items")]
    public List<GameObject> destructibleItems;

    [Header("Chunk configs")]
    public static MapController instance;
    public List<GameObject> terrainChunks;
    public GameObject player, currentChunk;
    public Transform chunksParent;
    public float checkerRadius; // how big of a radius before new chunk gets generated
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    private PlayerController playerController;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOptDistance, optimizerCooldownDuration = 1f; // must be greater than length and width of tilemap
    float opDistance, optimizerCooldown; // distance between chunk and player

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerController.instance)
        {
            playerController = PlayerController.instance;
        }
    }

    void Update()
    {
        // check for chunks only if player is alive
        if(player != null)
        {
            ChunkChecker();
            ChunkOptimizer();
        }
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }

        // player is moving right, generate chunk on the right
        if(playerController.movementDirections.x > 0 && playerController.movementDirections.y == 0)
        {
            CheckChunkConditions("Right");
        } else if(playerController.movementDirections.x < 0 && playerController.movementDirections.y == 0)
        {
            // left
            CheckChunkConditions("Left");
        }
        else if (playerController.movementDirections.x == 0 && playerController.movementDirections.y > 0)
        {
            // up
            CheckChunkConditions("Up");
        }
        else if (playerController.movementDirections.x == 0 && playerController.movementDirections.y < 0)
        {
            // down
            CheckChunkConditions("Down");
        }
        else if (playerController.movementDirections.x > 0 && playerController.movementDirections.y > 0)
        {
            // right up
            CheckChunkConditions("RightUp");
        }
        else if (playerController.movementDirections.x > 0 && playerController.movementDirections.y < 0)
        {
            // right down
            CheckChunkConditions("RightDown");
        }
        else if (playerController.movementDirections.x < 0 && playerController.movementDirections.y > 0)
        {
            // left up
            CheckChunkConditions("LeftUp");
        }
        else if (playerController.movementDirections.x < 0 && playerController.movementDirections.y < 0)
        {
            // left down
            CheckChunkConditions("LeftDown");
        }
    }

    void CheckChunkConditions(string staticPoint)
    {
        // if NO chunk exists in that direction
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(staticPoint).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(staticPoint).position;
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int randomChunk = Random.Range(0, terrainChunks.Count);
        GameObject newChunk = Instantiate(terrainChunks[randomChunk], noTerrainPosition, Quaternion.identity);
        newChunk.transform.SetParent(chunksParent);

        spawnedChunks.Add(newChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0)
        {
            optimizerCooldown = optimizerCooldownDuration;
        } else
        {
            return;
        }

        foreach(GameObject chunk in spawnedChunks)
        {
            opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);

            if (opDistance > maxOptDistance)
            {
                // disable chunk if its too far from player for optimization
                chunk.SetActive(false);
            } else
            {
                chunk.SetActive(true);
            }
        }
    }

    public void SpawnDestructibleItem(Vector3 spawnPosition)
    {
        int randomObject = Random.Range(0, destructibleItems.Count);
        GameObject newProp = Instantiate(destructibleItems[randomObject], spawnPosition, Quaternion.identity);
    }
}
