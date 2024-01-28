using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickupItem
{
    public float healAmount = 25f;
    public float commonDropChance = 0.2f, rareDropChance = 0.4f, legendaryDropChance = 0.8f;

    protected override void TriggerItemEffect()
    {
        PlayerHealthController.instance.Heal(healAmount);
        Destroy(gameObject); // make sure to remove the potion after picking it up
    }

    public override float GetDropChance()
    {
        switch(droppableRarity)
        {
            case "Common":
                return commonDropChance;
            case "Rare":
                return rareDropChance;
            case "Legendary":
                return legendaryDropChance;
            default:
                return 0f;
        }
    }
}
