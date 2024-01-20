using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController instance;
    public List<PlayerStatsValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount; // how many levels these stats have in total
    public int moveSpeedLevel, healthLeve, pickupRangeLevel, maxWeaponsLevel;

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
