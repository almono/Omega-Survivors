using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chest : DestructibleItem
{
    public enum ChestRarity {Common, Rare, Legendary, Mythic, Unique};
    public float spawnChance; // chance to be dropped/created
    public ChestRarity rarity;

    public override float GetSpawnChance()
    {
        switch (rarity)
        {
            case ChestRarity.Common:
                return 0.15f;
            case ChestRarity.Rare:
                return 0.08f;
            case ChestRarity.Legendary:
                return 0.04f;
            case ChestRarity.Unique:
                return 0.01f;
            default:
                return 0f;
        }
    }

    public override void DropItems()
    {
        // Calculate the total drop chance of all items in the loot table
        foreach (DropItem lootItem in dropItems)
        {
            totalDropChance += lootItem.GetDropChance();
        }

        // make sure that the lowest total is always 1f / 100%, in case only one item with 40% chance
        // would be available not doing that would make this item drop 100% of the time
        if(totalDropChance < 1f)
        {
            totalDropChance = 1f;
        }

        // Generate a random number between 0 and the total drop chance
        float randomValue = Random.Range(0f, totalDropChance);

        // Iterate through the loot table to find the dropped item
        foreach (DropItem lootItem in dropItems)
        {
            if (droppedItems < maxItemDropCount)
            {
                //Debug.Log("Random value: " + randomValue.ToString());
                //Debug.Log("Drop chance: " + lootItem.GetDropChance());
                if (randomValue < lootItem.GetDropChance())
                {
                    // Return the dropped item prefab
                    PickupItem newItemDrop = Instantiate(lootItem.item, new Vector3(chestPosition.x + Random.Range(-0.5f, 0.5f), chestPosition.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                    droppedItems++;
                }

                if (droppedItems >= maxItemDropCount)
                {
                    // if we already dropped max items then stop the checks
                    break;
                }

                randomValue -= lootItem.GetDropChance();
            }
        }

        // dropped items count ensures we always drop at least one items
        // if none of the loot items drop, a coin should be dropped
        if (droppedItems < maxItemDropCount)
        {
            while (droppedItems < maxItemDropCount)
            {
                Coin newCoin = Instantiate(coinDrop, new Vector3(chestPosition.x + Random.Range(-0.6f, 0.6f), chestPosition.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                SetCoinValueByRarity(newCoin);
                droppedItems++;
            }
        }
    }

    public void SetCoinValueByRarity(Coin coin)
    {
        int maxCoinValue = 1;

        switch (rarity)
        {
            case ChestRarity.Common:
                maxCoinValue = 3;
                break;
            case ChestRarity.Rare:
                maxCoinValue = 5;
                break;
            case ChestRarity.Legendary:
                maxCoinValue = 10;
                break;
            case ChestRarity.Unique:
                maxCoinValue = 25;
                break;
            default:
                maxCoinValue = 3;
                break;
        }

        coin.SetCoinValue(Mathf.RoundToInt(Random.Range(1, maxCoinValue)));
    }

    public ChestRarity GetChestRarity()
    {
        return rarity;
    }
}
