using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController instance;
    public List<PlayerStatsValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount; // how many levels these stats have in total
    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // set up list of level upgrades
        for(int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            // add 5 to every next cost
            moveSpeed.Add(new PlayerStatsValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatsValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }

        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatsValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    void Update()
    {
        // if display of upgrades is active then make sure everything is displaying correctly
        if(UIController.instance.levelUpPanel.activeSelf)
        {
            UpdateUpgradesDisplay();
        }    
    }

    public void UpdateUpgradesDisplay()
    {
        if(moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        } else
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            UIController.instance.rangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.rangeUpgradeDisplay.ShowMaxLevel();
        }

        if (maxWeaponsLevel < maxWeapons.Count - 1)
        {
            UIController.instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }
        else
        {
            UIController.instance.maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed()
    {
        // increase level -> spend coins
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateUpgradesDisplay();

        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void PurchaseHealth()
    {
        // increase level -> spend coins
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateUpgradesDisplay();

        // restore health equal to the health increase
        PlayerHealthController.instance.currentHealth += (health[healthLevel].value - health[healthLevel - 1].value);
        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
    }

    public void PurchasePickupRange()
    {
        // increase level -> spend coins
        pickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateUpgradesDisplay();

        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }

    public void PurchaseMaxWeapons()
    {
        // increase level -> spend coins
        maxWeaponsLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateUpgradesDisplay();

        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
    }
}

[System.Serializable]
public class PlayerStatsValue
{
    public int cost;
    public float value;

    public PlayerStatsValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
