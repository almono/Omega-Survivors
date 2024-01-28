using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    protected string droppableRarity;

    // on item pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TriggerItemEffect();
        }
    }

    public virtual float GetDropChance()
    {
        return 0f;
    }

    protected virtual void TriggerItemEffect()
    {
        return;
    }

    public void SetRarity(Chest chestRarity)
    {
        droppableRarity = chestRarity.rarity.ToString();
    }
}
