using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController instance;

    [Header("Stat upgrades with costs")]
    public List<PlayerStatsValue> moveSpeed;
    public List<PlayerStatsValue> health;
    public List<PlayerStatsValue> pickupRange;
    public List<PlayerStatsValue> maxWeapons;

    [Header("Max stat levels")]
    public int moveSpeedLevelCount;
    public int healthLevelCount;
    public int pickupRangeLevelCount;

    [Header("Current stat levels")]
    public int moveSpeedLevel;
    public int healthLevel;
    public int pickupRangeLevel;
    public int maxWeaponsLevel;

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
        PlayerCharacterSO selectedCharacterData = GameManager.instance.GetPlayerCharacter();

        if (selectedCharacterData)
        {
            // if there is a custom character selected then get base values and scalling from its SO
            moveSpeed = new List<PlayerStatsValue>();
            health = new List<PlayerStatsValue>();
            pickupRange = new List<PlayerStatsValue>();
            maxWeapons = new List<PlayerStatsValue>();

            // prepare starting stats
            moveSpeed.Add(new PlayerStatsValue(0, selectedCharacterData.moveSpeed));
            health.Add(new PlayerStatsValue(0, selectedCharacterData.health));
            pickupRange.Add(new PlayerStatsValue(0, selectedCharacterData.pickupRange));
            maxWeapons.Add(new PlayerStatsValue(0, selectedCharacterData.maxWeapons));

            for (int i = 1; i < moveSpeedLevelCount; i++)
            {
                moveSpeed.Add(new PlayerStatsValue(
                    selectedCharacterData.moveSpeedStartingCost + (i * selectedCharacterData.moveSpeedCostIncrement), 
                    moveSpeed[i - 1].value * selectedCharacterData.moveSpeedIncrement
                ));
            }

            for (int i = 1; i < healthLevelCount; i++)
            {
                health.Add(new PlayerStatsValue(
                    selectedCharacterData.healthStartCost + (i * selectedCharacterData.healthCostIncrement),
                    health[i - 1].value + selectedCharacterData.healthIncrement
                ));
            }

            for (int i = 1; i < healthLevelCount; i++)
            {
                pickupRange.Add(new PlayerStatsValue(
                    selectedCharacterData.pickupRangeStartingCost + (i * selectedCharacterData.pickupRangeCostIncrement),
                    pickupRange[i - 1].value * selectedCharacterData.pickupRangeIncrement
                ));
            }

            // make sure you can upgrade max levels only till you can get all of the weapons
            int maxPlayerWeapons = PlayerController.instance.unassignedWeapons.Count - (selectedCharacterData.maxWeapons - PlayerController.instance.assignedWeapons.Count);

            for (int i = 1; i < maxPlayerWeapons; i++)
            {
                maxWeapons.Add(new PlayerStatsValue(
                    maxWeapons[i - 1].cost == 0 ? Mathf.RoundToInt(selectedCharacterData.maxWeaponsStartingCost * selectedCharacterData.maxWeaponsCostIncrement) : Mathf.RoundToInt(maxWeapons[i - 1].cost * selectedCharacterData.maxWeaponsCostIncrement),
                    maxWeapons[i - 1].value + 1
                ));
            }
        } else
        {
            SetupStatLists();
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

    public void SetupStatLists()
    {
        // set up list of level upgrades
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
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
