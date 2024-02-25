using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributePickups : PickupItem
{
    public TempBuffSO buffValues;
    public PickupType pickupType;
    public string pickupName;

    protected override void TriggerItemEffect()
    {
        switch(pickupType)
        {
            case PickupType.Potion:
                TempBuffController.instance.ApplyBuff(this);
                break;
            default: 
                break;
        }

        Destroy(gameObject); // make sure to remove the pickup after picking it up
    }

    public TempBuffSO getBuffValues()
    {
        return buffValues;
    }
}

public enum PickupType
{
    Potion
}
