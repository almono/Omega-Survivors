using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : PickupItem
{
    public float speedBuffValue = 1.5f, speedBuffDuration = 60f;

    protected override void TriggerItemEffect()
    {
        TempBuffController.instance.ApplySpeedBuff(speedBuffValue, speedBuffDuration);
        Destroy(gameObject); // make sure to remove the potion after picking it up
    }
}
