using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempBuffController : MonoBehaviour
{
    public static TempBuffController instance;
    public List<TempBuffSO> activeBuffs = new List<TempBuffSO>();

    [Header("Speed Buff")]
    public float moveSpeedBuffCounter = 0f;
    public float moveSpeedBuffMultiplier = 1f;

    [Header("Damage Buff")]
    public float damageBuffCounter = 0f;
    public float damageBuffMultiplier = 1f;

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

    private void Start()
    {

    }

    void Update()
    {
        CheckTempBuffsStatus();
    }

    public void ApplyBuff(TempBuffSO booster, GameObject UIIcon, TMP_Text buffCounterText)
    {
        // only perform the checks if the buff does not exist already
        if(!activeBuffs.Contains(booster))
        {
            switch (booster.attributeToBoost)
            {
                case AttributeType.Speed:
                    UIIcon.SetActive(true);
                    activeBuffs.Add(booster);
                    moveSpeedBuffCounter = booster.boostDuration;
                    moveSpeedBuffMultiplier = booster.boostValue;
                    break;
                case AttributeType.Damage:
                    UIIcon.SetActive(true);
                    activeBuffs.Add(booster);
                    damageBuffCounter = booster.boostDuration;
                    damageBuffMultiplier = booster.boostValue;
                    break;
                default:
                    break;
            }
        }
    }

    public void CheckTempBuffsStatus()
    {
        /*foreach(TempBuffSO buff in activeBuffs)
        {
            buff.boostDuration -= Time.deltaTime;
            buff.buffDurationText.text = buff.boostDuration.ToString("00");

            if (buff.boostDuration <= 0)
            {
                buff.buffIcon.SetActive(false);
                activeBuffs.Remove(buff);

                switch (buff.attributeToBoost)
                {
                    case AttributeType.Speed:
                        moveSpeedBuffMultiplier = 1f;
                        break;
                    case AttributeType.Damage:
                        damageBuffMultiplier = 1f;
                        break;
                    default:
                        break;
                }
            }
        }*/
    }
}
