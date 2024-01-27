using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public float dropChance = 0.5f;

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
        return dropChance;
    }

    protected virtual void TriggerItemEffect()
    {
        return;
    }
}
