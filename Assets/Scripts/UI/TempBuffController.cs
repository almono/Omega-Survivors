using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempBuffController : MonoBehaviour
{
    public static TempBuffController instance;
    public List<String> activeBuffs = new List<String>();

    [Header("Speed Buff")]
    public float originalSpeed = 0f;
    public float moveSpeedBuffMultiplier = 1f;

    [Header("Damage Buff")]
    public float originalDamage = 0f;
    public float damageBuffMultiplier = 1f;

    [Header("Crit Chance Buff")]
    public float originalCritChance = 0f;
    public float critChanceMultiplier = 0f;

    [Header("Crit Multiplier Buff")]
    public float originalCritMultiplier = 0f;
    public float critMultiplierValue = 0f;

    [Header("Buff Prefab")]
    public GameObject buffHolder, activeBuff;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyBuff(AttributePickups booster)
    {
        // only perform the checks if the buff does not exist already
        if(!activeBuffs.Contains(booster.pickupName))
        {
            activeBuffs.Add(booster.pickupName); // add this name to active buffs list

            GameObject newBuff = Instantiate(activeBuff, transform.position, Quaternion.identity, buffHolder.transform);
            ActiveBuff buffData = newBuff.GetComponent<ActiveBuff>();
            buffData.SetIcon(booster.pickupName);
            buffData.buffDuration = booster.buffValues.boostDuration;
            buffData.maxBuffDuration = booster.buffValues.maxDuration;
            newBuff.transform.name = booster.pickupName;
            newBuff.SetActive(true);

            switch (booster.buffValues.attributeToBoost)
            {
                case AttributeType.Speed:
                    moveSpeedBuffMultiplier = booster.buffValues.boostValue;
                    break;
                case AttributeType.Damage:
                    damageBuffMultiplier = booster.buffValues.boostValue;
                    break;
                case AttributeType.CritChance:
                    critChanceMultiplier = booster.buffValues.boostValue;
                    break;
                case AttributeType.CritMultiplier:
                    critMultiplierValue = booster.buffValues.boostValue;
                    break;
                default:
                    break;
            }
        } else
        {
            ActiveBuff existingBuff = buffHolder.transform.Find(booster.pickupName).gameObject.GetComponent<ActiveBuff>();
            if (existingBuff != null)
            {
                existingBuff.buffDuration = existingBuff.maxBuffDuration; // refresh buff duration
            }
        }
    }

    public void RemoveBuff(string buffName)
    {
        if(activeBuffs.Contains(buffName))
        {
            activeBuffs.Remove(buffName);
        }
    }
}
