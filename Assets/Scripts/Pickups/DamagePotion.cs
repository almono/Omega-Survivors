using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion : PickupItem
{
    public float attackBuffValue = 1.2f, attackBuffDuration = 30f;

    protected override void TriggerItemEffect()
    {
        TempBuffController.instance.ApplyDamageBuff(attackBuffValue, attackBuffDuration);
        Destroy(gameObject); // make sure to remove the potion after picking it up
    }
}
