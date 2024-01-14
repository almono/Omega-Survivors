using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    public Sprite weaponIcon;

    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp()
    {
        if(weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, attackCooldown, amount, duration;
    public string upgradeText;
}
