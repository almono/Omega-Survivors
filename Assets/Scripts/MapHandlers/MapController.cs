using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius; // how big of a radius before new chunk gets generated
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    private PlayerController playerController;

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
        }
    }

    void ChunkChecker()
    {
        // player is moving right
        if(playerController.movementDirections.x > 0 && playerController.movementDirections.y == 0)
        {
            // if NO chunk exists in that direction
            if(!Physics2D.OverlapCircle(player.transform.position + new Vector3(36f, 0f, 0f), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(36f, 0f, 0f);
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int randomChunk = Random.Range(0, terrainChunks.Count);
        GameObject newChunk = Instantiate(terrainChunks[randomChunk], noTerrainPosition, Quaternion.identity);
    }
}
