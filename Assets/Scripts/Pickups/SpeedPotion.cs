using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedPotion : PickupItem
{
    public GameObject buffIcon;
    public TMP_Text buffText;
    public TempBuffSO buffValues;

    protected override void TriggerItemEffect()
    {
        TempBuffController.instance.ApplyBuff(buffValues, buffIcon, buffText);
        Destroy(gameObject); // make sure to remove the potion after picking it up
    }
}
