using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite playerIcon; // to be used on character selection screen

    public string GetName()
    {
        return characterName;
    }

    public Sprite GetPlayerIcon()
    {
        return playerIcon;
    }
}
