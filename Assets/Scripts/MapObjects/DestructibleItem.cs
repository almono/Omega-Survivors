using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using static UnityEditor.Progress;

public class DestructibleItem : MonoBehaviour
{
    public int currentHealth = 3;
    public int hitsToDestroy = 3, maxItemDropCount = 1;
    public bool shouldDropItems = false;
    public List<DropItem> dropItems;

    protected int droppedItems = 0;
    protected float totalDropChance = 0f;
    protected Vector3 chestPosition;

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

            if(shouldDropItems)
            {
                DropItems();
            }
        }
    }

    public virtual void DropItems()
    {
        return;
    }

    public virtual float GetSpawnChance()
    {
        return 0f;
    }

    public string GetName()
    {
        return this.name;
    }
}

[System.Serializable]
public class DropItem
{
    public PickupItem item;
    public float dropChance = 0.1f;

    public float GetDropChance()
    {
        return dropChance;
    }
}
