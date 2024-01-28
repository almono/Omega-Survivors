using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    public GameObject targetMap;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            MapController.instance.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(targetMap != null && MapController.instance.currentChunk == targetMap)
            {
                MapController.instance.currentChunk = null;
            }
        }
    }
}
