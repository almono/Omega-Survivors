using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickupItem
{
    public float healAmount = 25f;

    protected override void TriggerItemEffect()
    {
        PlayerHealthController.instance.Heal(healAmount);
        Destroy(gameObject); // make sure to remove the potion after picking it up
    }

    public override float GetDropChance()
    {
        return base.GetDropChance();
    }
}
