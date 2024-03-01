using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "New Character Stats")]

public class PlayerCharacterSO : ScriptableObject
{
    public string characterName = "Character";

    [Header("Speed Data")]
    public float moveSpeed = 4f; // 4f default
    public float moveSpeedIncrement = 1.1f;
    public int moveSpeedStartingCost = 5, moveSpeedCostIncrement = 5;

    [Header("Health Data")]
    public int health = 100; // 100 default
    public int healthIncrement = 10, healthStartCost = 25, healthCostIncrement = 25;

    [Header("Pickup Data")]
    public float pickupRange = 1.5f; // 1.5f default
    public float pickupRangeIncrement = 1.15f;
    public int pickupRangeStartingCost = 50, pickupRangeCostIncrement = 25;

    [Header("Crit Data")]
    public float critChance = 0f; // default is 0f
    public float critChanceIncrement = 0.05f;
    public int critChangeStartingCost = 100, critChanceCostIncrement = 100;

    public float critMultiplier = 2f, critMultiplierIncrement = 0.5f; // by default dmg x2
    public int critMultiplierStartingCost = 50, critMultiplierCostIncremet = 75;

    [Header("Weapon Data")]
    public int maxWeapons = 3; // 3 default
    public int maxWeaponsStartingCost = 500;
    public float maxWeaponsCostIncrement = 3f; // x3 with each level

    public BaseWeapon usedWeapon; // weapon the player would start with
    public Image playerIcon; // to be used on character selection screen

    public string GetName()
    {
        return characterName;
    }

    public Image GetPlayerIcon()
    {
        return playerIcon;
    }

    public float GetSpeed() { return moveSpeed; }
    public float GetPickupRange() { return pickupRange; }
    public float GetCritChance() {  return critChance; }
    public float GetCritMultiplier() {  return critMultiplier; }
    public int GetMaxWeapons() {  return maxWeapons; }
    public BaseWeapon GetUsedWeapon() {  return usedWeapon; }
}
