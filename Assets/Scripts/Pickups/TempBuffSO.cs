using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTempBoostItem", menuName = "Temporary Boost Item")]

public class TempBuffSO : ScriptableObject
{
    public string itemName = "Temporary Boost";
    public AttributeType attributeToBoost;
    public float boostDuration = 30f;
    public float maxDuration = 30f;
    public float boostValue = 1.5f;

    public GameObject GetBuffIcon(SpeedPotion parentObject)
    {
        return parentObject.buffIcon;
    }

    public TMP_Text GetBuffText(SpeedPotion parentObject)
    {
        return parentObject.buffText;
    }
}

public enum AttributeType
{
    Speed,
    Damage
}