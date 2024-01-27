using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using static UnityEditor.Progress;

public class DestructibleItem : MonoBehaviour
{
    public int currentHealth = 3;
    public int hitsToDestroy = 3, maxItemDropCount = 1;
    private int droppedItems = 0;
    public List<PickupItem> dropItems;
    private float totalDropChance = 0f;
    private Vector3 chestPosition;

    public Coin coinDrop;

    private void Start()
    {
        currentHealth = hitsToDestroy;
        chestPosition = transform.position;
        coinDrop = CoinController.instance.coin;
    }

    public void TakeHit()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            DropItems();
        }
    }

    public void DropItems()
    {
        // dropped items count ensures we always drop at least one items
        // if none of the loot items drop a coin should be dropped
        
        // Calculate the total drop chance of all items in the loot table
        foreach (PickupItem lootItem in dropItems)
        {
            totalDropChance += lootItem.GetDropChance();
        }

        // Generate a random number between 0 and the total drop chance
        float randomValue = Random.Range(0f, totalDropChance);

        // Iterate through the loot table to find the dropped item
        foreach (PickupItem lootItem in dropItems)
        {
            if(droppedItems < maxItemDropCount)
            {
                if (randomValue < lootItem.GetDropChance())
                {
                    // Return the dropped item prefab
                    PickupItem newItemDrop = Instantiate(lootItem, new Vector3(chestPosition.x + Random.Range(-0.3f, 0.3f), chestPosition.y + Random.Range(-0.3f, 0.3f)), Quaternion.identity);
                    droppedItems++;
                }

                randomValue -= lootItem.GetDropChance();
            }
        }

        if(droppedItems < maxItemDropCount)
        {
            while(droppedItems < maxItemDropCount)
            {
                Instantiate(coinDrop, new Vector3(chestPosition.x + Random.Range(-0.6f, 0.6f), chestPosition.y + Random.Range(-0.3f, 0.3f)), Quaternion.identity);
                droppedItems++;
            }
        }

        // If no item was selected (this may happen due to floating-point imprecision), return null
        return;
    }
}
