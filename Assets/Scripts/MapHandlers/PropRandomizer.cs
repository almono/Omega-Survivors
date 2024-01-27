using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    public float propSpawnChance = 0.7f, destructibleSpawnChance = 0.05f;

    void Start()
    {
        SpawnProps();
    }

    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach(GameObject propPoint in propSpawnPoints)
        {
            // pick random prop
            int randomProp = Random.Range(0, propPrefabs.Count);

            // by default 30% change to spawn a prop at given point
            // If a prop is to be spawned then we check if thats a normal prop or a destructible object like chest, crate etc.
            if(Random.Range(0f, 1f) < propSpawnChance)
            {
                if(Random.Range(0f, 1f) < destructibleSpawnChance)
                {
                    MapController.instance.SpawnDestructibleItem(propPoint.transform.position);
                } else
                {
                    GameObject newProp = Instantiate(propPrefabs[randomProp], propPoint.transform.position, Quaternion.identity);
                    newProp.transform.SetParent(propPoint.transform);
                }
            }
        }
    }
}
