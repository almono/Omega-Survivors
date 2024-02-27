using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "New Character Stats")]

public class PlayerCharacterSO : ScriptableObject
{
    public string characterName = "Character";

    public float moveSpeed = 4f; // 4f default
    public float pickupRange = 1.5f; // 1.5f default
    public float critChance = 0f; // default is 0f
    public float critMultiplier = 2f; // by default dmg x2
    public int maxWeapons = 3; // 3 default

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
